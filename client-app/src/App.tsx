import React, { useEffect, useState } from "react";
// import logo from "./logo.svg";
import "./App.css";
import axios from "axios";

function App() {
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    axios.get("http://localhost:5000/activities").then((response) => {
      console.log(response);
      setActivities(response.data);
    });
  }, []); //add and array of dependencies so that our useEffect will run only once

  return (
    <div className="App">
      <header className="App-header">
        <ul>
          {activities.map((activity: any) => (
            <li key={activity.id}>{activity.title}</li>
          ))}
        </ul>
      </header>
    </div>
  );
}

export default App;
