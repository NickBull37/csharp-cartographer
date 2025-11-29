import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Navbar } from "./components";
import { DemoUploadChoice, DemoOptionSelection, CartographerDemo, UserCodeUpload, UserCodeStaging, UserCodeCartograph } from "./pages";

function App() {

    const [artifact, setArtifact] = useState(null);

    // useEffect(() => {
    //     console.log("Artifact: ", artifact);
    // }, [artifact]);

    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="/" element={<DemoUploadChoice />} />
                <Route path="/demo-options" element={<DemoOptionSelection />} />
                <Route path="/cartographer-demo" element={<CartographerDemo />} />

                <Route path="/upload" element={<UserCodeUpload setArtifact={setArtifact} />} />
                <Route path="/staging" element={<UserCodeStaging artifact={artifact} />} />
                <Route path="/cartograph" element={<UserCodeCartograph artifact={artifact} />} />
                {/* <Route path="/landing-page" element={<LandingPage />} /> */}
            </Routes>
        </BrowserRouter>
    );
}

export default App;
