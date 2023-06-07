import { useState, useEffect } from 'react';
import {
  projectAuth,
  projectStorage,
  projectFirestore,
} from '../firebase/config';
import { useAuthContext } from './useAuthContext';

export const useSignup = () => {
  const [isCancelled, setIsCancelled] = useState(false);
  const [error, setError] = useState(null);
  const [isPending, setIsPending] = useState(false);
  const { dispatch } = useAuthContext();

  const signup = async (email, password, displayName) => {
    setError(null);
    setIsPending(true);

    try {
      // signup
      const res = await projectAuth.createUserWithEmailAndPassword(
        email,
        password
      );

      if (!res) {
        throw new Error('Could not complete signup');
      }

      // add display name to user
      await res.user.updateProfile({ displayName });

      const user = {
        firebaseId: res.user.uid,
        fullName: res.user.displayName,
        email: res.user.email,
        username: '',
        //id: res.user.id,
      };

      //create a user document
      await projectFirestore
        .collection('users')
        .doc(res.user.uid)
        .set({
          online: true,
          displayName,
        });

      // dispatch login action
      dispatch({ type: 'LOGIN', payload: res.user });

      await fetch('https://localhost:7013/api/users', {
        method: 'POST',

        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(user),
      })
        .then((res) => res.json())
        .then((createdUser) => {
          if (createdUser.hasOwnProperty('id')) {
            localStorage.setItem(
              'capstone_user',
              JSON.stringify({
                id: createdUser.id,
              })
            );

            //navigate('/');
          }
        });

      if (!isCancelled) {
        setIsPending(false);
        setError(null);
      }
    } catch (err) {
      if (!isCancelled) {
        setError(err.message);
        setIsPending(false);
      }
    }
  };

  useEffect(() => {
    return () => setIsCancelled(true);
  }, []);

  return { signup, error, isPending };
};
