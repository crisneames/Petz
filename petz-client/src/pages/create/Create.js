import './Create.css';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import axios from 'axios';

import { useAuthContext } from '../../hooks/useAuthContext';

const Create = () => {
  const { user } = useAuthContext;
  //  posts table =Id, Post, Date, ImageUrl, UserId
  //  users table - Id, FirebaseId, FullName, Email, Username, Password
  //const [fullname, setFullname] = useState('');
  const [post, setPost] = useState('');
  const [date, setDate] = useState('');
  const [imageUrl, setImageUrl] = useState(null);
  const [imageError, setImageError] = useState(null);

  // Created Post - we need a pet Id, and two fetches, 1 from posts and the second from pets

  const [file, setFile] = useState();

  function handleChangeFile(event) {
    setFile(event.target.files[0]);
  }

  const [postSubmit, setPostSubmit] = useState('');
  const [pet, setPet] = useState('');
  const [userPets, setUserPets] = useState([]);

  const navigate = useNavigate();

  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/posts`);
      const postArray = await response.json();
      setPostSubmit(postArray);

      const resp = await fetch(
        `https://localhost:7013/api/pets/user/${userObject.id}`
      );
      const userPetsArray = await resp.json();
      setUserPets(userPetsArray);
    };
    fetchData();
    //console.warn();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(date, post, imageUrl);
    const postSubmit = {
      date: new Date(),
      post: post,
      imageUrl: imageUrl,
      userId: userObject.id,
    };

    const response = await fetch(`https://localhost:7013/api/posts`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(postSubmit),
    });

    console.log('POST SUB', postSubmit);
    const addedPost = await response.json();

    console.log('ADDED POST', addedPost);

    if (pet) {
      const petPost = {
        // replace form field below
        petId: pet,
        postId: addedPost.id,
      };

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
      <h2 className="page-title">Create a new post</h2>

      <form onSubmit={handleSubmit}>
        {/* <label>
          <span>Username: </span>
          <input
            required
            type="text"
            onChange={(e) => setUsername(e.target.value)}
            value={username}
          />
        </label> */}
        {/* <Image src={user.imageUrl} /> */}
        {/* <label>
          <span>Date: </span>
          <input
            required
            type="date"
            onChange={(e) => setDate(e.target.value)}
            value={date}
          />
        </label> */}
        <label>
          <span>Post: </span>
          <textarea
            required
            type="textarea"
            onChange={(e) => setPost(e.target.value)}
            value={post}
          ></textarea>
        </label>

        <label>
          <span>Pet Name: </span>
          <select
            type="select"
            onChange={(e) => setPet(e.target.value)}
            // value={pet}
          >
            <option value="">-- Choose --</option>
            {userPets.map((p) => {
              return (
                <option key={p.id} value={p.id}>
                  {p.name}
                </option>
              );
            })}
          </select>
        </label>
        <label>
          <span>Image: </span>
          <input
            type="text"
            value={postSubmit.imageUrl}
            onChange={(e) => setImageUrl(e.target.value)}
          />

          {/* {imageError && <div className="error">{imageError}</div>} */}
        </label>

        <button className="btn">Add Post</button>
      </form>
    </div>
  );
};
export default Create;
