import './AddPet.css';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import { useAuthContext } from '../../hooks/useAuthContext';

const AddPet = () => {
  const { user } = useAuthContext;
  // const [postSubmit, setPostSubmit] = useState('');

  const [pet, setPet] = useState('');
  // const [userPets, setUserPets] = useState([]);

  const [petName, setPetName] = useState({
    name: '',
  });

  const navigate = useNavigate();

  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/pets`);
      const petArray = await response.json();
      setPetName(petArray);
    };
    fetchData();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const postSubmit = {
      name: petName,
      userId: userObject.id,
    };
    // console.log(petName, userObject.id);

    const response = await fetch(`https://localhost:7013/api/pets`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(postSubmit),
    });
    const addedPost = await response.json();

    console.log('ADDED POST', addedPost);

    if (pet) {
      const petPost = {
        // replace form field below
        petId: pet.id,
        postId: addedPost.id,
      };
      console.log('PET POST', petPost);
      await fetch(`https://localhost:7013/api/posts/pets`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(petPost),
      });
    }
    navigate('/');
  };

  return (
    <div className="create-form">
      <h2 className="page-title">Pet Name</h2>

      <form onSubmit={handleSubmit}>
        <label>
          <span>Pet Name: </span>
          <input
            required
            type="text"
            onChange={(e) => setPetName(e.target.value)}
            value={petName.name}
          />
        </label>
        <button className="btn">Add Name</button>
      </form>
    </div>
  );
};
export default AddPet;
