import { useNavigate, useParams } from 'react-router';
import { useState, useEffect } from 'react';
import './Post.css';

export const Post = () => {
  const [postSelect, setPostSelect] = useState('');
  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  console.log('user obj', userObject);

  //let counter = 0;

  const { id } = useParams();
  const navigate = useNavigate();

  const [petName, setPetName] = useState('');

  const [post, updatePost] = useState({
    id: 0,
    post: '',
    date: '',
    imageUrl: '',
    userId: userObject.id,
    pet: [],
  });

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/posts/${id}`);
      const post = await response.json();
      updatePost(post);
    };

    fetchData();

    //fetchData2();
  }, [id]);
  console.log('POST', post);
  console.log('ID', id);
  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(
        `https://localhost:7013/api/Posts/PostWithPets/${id}`
      );
      const postArray = await response.json();
      setPetName(postArray);
    };
    fetchData();
  }, [id]);

  // useEffect(() => {

  //   };
  //   fetchData();
  // }, []);
  //console.log('PETNAME', petName);

  const handleEditRecipe = (e) => {
    e.preventDefault();

    const savePost = async () => {
      const options = {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(post),
      };
      const resp = await fetch(
        `https://localhost:7013/api/Posts/${id}`,
        options
      );

      await resp.json();
      navigate('/');
    };
    savePost();
  };

  const handleDelete = async () => {
    const response = await fetch(`https://localhost:7013/api/posts/${id}`, {
      method: 'DELETE',
    });
    // const delete2 = await fetch(
    //   `https://localhost:7013/api/Posts/PostWithPets/${id}`,
    //   {
    //     method: 'DELETE',
    //   }
    // );
  };

  //const url = 'https://localhost:7013/api/posts/' + id;
  //const url2 = 'https://localhost:7013/api/Posts/PostWithPets/' + id;
  // // Delete Recipe
  // const handleDelete = () => {
  //   fetch(url, {
  //     method: 'DELETE',
  //   }).then(() => {
  //     navigate('/');
  //   });
  // };

  return (
    <div className="Post">
      <form>
        {/* <label>
          <span>Date: </span>
          <input
            required
            value={new Date(post?.date).toLocaleString()}
            type="date"
            dateFormat="mm/dd/yyyy"
            onChange={(e) => {
              const copy = { ...post };
              copy.date = e.target.value;
              updatePost(copy);
            }}
          />
        </label> */}
        <label>
          <span>Post: </span>
          <textarea
            required
            value={post.post}
            type="textarea"
            onChange={(e) => {
              const copy = { ...post };
              copy.post = e.target.value;
              updatePost(copy);
            }}
          ></textarea>
        </label>

        {/* <label htmlFor="pet-name">
          <span>Pet Name: </span>
        </label>
        <select
          id="pet-name"
          type="select"
          value={petName}
          onChange={(e) => {
            const copy = { ...petName };
            copy.pet = e.target.value;
            setPetName(copy);
          }}
        >
          <option value="default">--Please choose an option--</option>

          {post &&
            post.pet &&
            post.pet.map((p) => {
              //  console.log('TYPE', type);

              return <option value={p.name}>{p.name}</option>;

              // <option key={++counter} value={p.name}>
              //   {p.name}
              // </option>
              // );
            })}
        </select> */}

        <button
          className="btn"
          onClick={(clickEvent) => handleEditRecipe(clickEvent)}
        >
          Update Post
        </button>
        <button className="btn" onClick={handleDelete}>
          Delete
        </button>
      </form>
    </div>
  );
};
export default Post;
