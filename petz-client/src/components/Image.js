function Image({ src }) {
  return (
    <div className="image">
      <img src={src} alt="Pet Image" />
    </div>
  );
}

export default Image;
