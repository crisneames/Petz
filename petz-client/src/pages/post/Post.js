import { useParams } from 'react-router';
import { useState, useEffect } from 'react';
import './Post.css';

export const Post = () => {
  const [postSelect, setPostSelect] = useState('');
  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  const { id } = useParams;
  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/posts?id=${id}`);
      const selectPost = await response.json();
      setPostSelect(selectPost);
    };
    fetchData();
    //console.warn();
  }, []);
  return <div className="Post"></div>;
};
export default Post;
