//import { userInfo } from 'os';
import './Create.css';
//import React from 'react';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
//import FileUploader from '../../components/FileUploader';

import { useAuthContext } from '../../hooks/useAuthContext';

const Create = () => {
  const { user } = useAuthContext;
  //  posts table =Id, Post, Date, ImageUrl, UserId
  //  users table - Id, FirebaseId, FullName, Email, Username, Password
  const [fullname, setFullname] = useState('');
  const [post, setPost] = useState('');
  const [date, setDate] = useState('');
  const [imageUrl, setImageUrl] = useState(null);
  const [imageError, setImageError] = useState(null);

  // Created Post - we need a pet Id, and two fetches, 1 from posts and the second from pets

  const [postSubmit, setPostSubmit] = useState('');

  const navigate = useNavigate();

  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  // const handleSubmit = (e) => {
  //   e.preventDefault();
  //   console.log(username, date, post);
  // };

  // fileSelectedHandler = (e) => {
  //   setImageUrl(imageUrl);
  // };

  const handleFileChange = (e) => {
    setImageUrl(null);
    let selected = e.target.files[0];
    console.log(selected);

    if (!selected) {
      setImageError('Please select an image');
      return;
    }

    if (!selected.type.includes('image')) {
      setImageError('Selected file must be an image');
      return;
    }

    if (selected.size > 150000) {
      setImageError('Image file size must be less than 150kb');
      return;
    }

    setImageError(null);
    setImageUrl(selected);
    console.log('image updated');
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/posts`);
      const postArray = await response.json();
      setPostSubmit(postArray);
    };
    fetchData();
    //console.warn();
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log(date, post, imageUrl);
    const postSubmit = {
      //fullname: fullname,
      date: date,
      post: post,
      imageUrl: imageUrl,
      userId: userObject.id,
      // id: userObject.id,
    };

    fetch(`https://localhost:7013/api/posts`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(postSubmit),
    }).then((addedPost) => {
      const petPost = {
        // replace form field below
        petId: 3,
        postId: addedPost.Id,
      };
      fetch(`https://localhost:7013/api/posts/pets`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(petPost),
      });
      navigate('/');
    });
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
        <label>
          <span>Date: </span>
          <input
            required
            type="date"
            onChange={(e) => setDate(e.target.value)}
            value={date}
          />
        </label>
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
          <span>Image: </span>
          <input type="file" onChange={handleFileChange} />
          {/* value{imageUrl} */}
          {imageError && <div className="error">{imageError}</div>}
        </label>
        {/* <FileUploaded
          onFileSelectSuccess={(file) => setSelectedFile(file)}
          onFileSelectError={({ error }) => alert(error)}
        /> */}
        <button className="btn">Add Post</button>
      </form>
    </div>
  );
};
export default Create;
