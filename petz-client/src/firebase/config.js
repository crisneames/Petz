import firebase from 'firebase/app';
import 'firebase/firestore';
import 'firebase/auth';

const firebaseConfig = {
  apiKey: 'AIzaSyB1NxWMTszsBqA0rvo59naeCbQQdIcshHk',
  authDomain: 'petz-aa6d6.firebaseapp.com',
  projectId: 'petz-aa6d6',
  storageBucket: 'petz-aa6d6.appspot.com',
  messagingSenderId: '121518444832',
  appId: '1:121518444832:web:279d15da56a0e2b9c7c745',
};

// initialize firebase
firebase.initializeApp(firebaseConfig);

//initialize services
const projectFirestore = firebase.firestore();
const projectAuth = firebase.auth();

// timestamp
const timestamp = firebase.firestore.timestamp;

export { projectFirestore, projectAuth, timestamp };
