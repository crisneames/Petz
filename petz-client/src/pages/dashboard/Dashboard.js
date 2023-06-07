import './Dashboard.css';
import { Link, useParams } from 'react-router-dom';
import { useState, useEffect } from 'react';
import Image from '../../components/Image';
import { useAuthContext } from '../../hooks/useAuthContext';

export const Dashboard = () => {
  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  const { user } = useAuthContext();

  const [posts, setPosts] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(`https://localhost:7013/api/posts`);
      const data = await response.json();
      setPosts(data);
    };
    fetchData();
    //console.warn();
  }, []);

  return (
    <div>
      <h2 className="page-title">Posts</h2>
      {posts.map((post) => (
        <div className="post-list">
          <Link to={`/posts/${post.id}`} key={post.id}>
            {/* <p> {post.fullName}</p> */}
            <p key={post.id}> {post.date}</p>
            <p>{post.post}</p>
            <p>{<Image />}</p>
          </Link>
        </div>
      ))}
    </div>
  );
};
export default Dashboard;
