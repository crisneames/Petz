import './Dashboard.css';
import { Link } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { useAuthContext } from '../../hooks/useAuthContext';
import { formatInTimeZone } from 'date-fns-tz';

export const Dashboard = () => {
  const localUser = localStorage.getItem('capstone_user');
  const userObject = JSON.parse(localUser);

  const { user } = useAuthContext();

  const [posts, setPosts] = useState([]);

  const formatDateTime = (postDateTime) => {
    const convertDateTime = new Date(postDateTime);

    return formatInTimeZone(convertDateTime, 'America/Chicago', 'LLLL d, yyy');
  };

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(
        `https://localhost:7013/api/posts/postswithpets`
      );
      const data = await response.json();
      setPosts(data);
    };
    fetchData();
    //console.warn();
  }, []);

  return (
    <div>
      <h2 className="page-title">Posts</h2>
      <div className="container">
        {posts.map((post) => (
          <div className="post-list">
            {userObject.id === post.userId ? (
              <Link to={`/posts/${post.id}`} key={post.id}>
                {/* <p> {post.user.fullName}</p> */}
                <p key={post.id}> {formatDateTime(post.date)}</p>
                <p>{post.post}</p>
                <p>
                  {post.pet?.map((pet) => {
                    return pet.name;
                  })}
                </p>
                <img alt="Pet Image" src={post.imageUrl} />
              </Link>
            ) : (
              <div>
                <p key={post.id}> {formatDateTime(post.date)}</p>
                <p>{post.post}</p>
                <p>
                  {post.pet?.map((pet) => {
                    return pet.name;
                  })}
                </p>
                <img alt="Pet Image" src={post.imageUrl} />{' '}
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};
export default Dashboard;
