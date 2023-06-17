import { useNavigate, useParams } from 'react-router';
import { useState, useEffect } from 'react';
import './Post.css';

export const Post = () => {
  // const [postSelect, setPostSelect] = useState('');
  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  console.log('user obj', userObject);

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
  }, [id]);

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
  };

  return (
    <div className="Post">
      <form>
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
