import React from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';

function Home() {
    return (
        <div>
            <h1 style={{ marginBottom: '30px', fontSize: '80px' }}>ScamBet</h1>
            <p className="fs-6" style={{ marginTop: '10px' }}>
            </p>
        </div>
    );
}
ReactDOM.render(<Home />, document.getElementById('Home-container'));

export default Home;
