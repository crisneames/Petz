import { useState, useEffect } from 'react';
import { projectAuth, projectFirestore } from '../firebase/config';
import { useAuthContext } from './useAuthContext';

export const useLogin = () => {
  const [isCancelled, setIsCancelled] = useState(false);
  const [error, setError] = useState(null);
  const [isPending, setIsPending] = useState(false);
  const { dispatch } = useAuthContext();

  const login = async (email, password) => {
    setError(null);
    setIsPending(true);

    try {
      // login
      const res = await projectAuth.signInWithEmailAndPassword(email, password);

      // update online status

      await projectFirestore
        .collection('users')
        .doc(res.user.uid)
        .update({ online: true });
      // sign the user out

      // dispatch login action
      dispatch({ type: 'LOGIN', payload: res.user });

      await fetch(`https://localhost:7013/api/users/${res.user.uid}`, {
        headers: {
          'Content-Type': 'application/json',
        },
      })
        .then((res) => res.json())
        .then((user) => {
          if (user.hasOwnProperty('id')) {
            localStorage.setItem(
              'capstone_user',
              JSON.stringify({
                id: user.id,
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

  return { login, isPending, error };
};
