import { useState } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Navbar } from "./components";
import { DemoUploadChoice, DemoOptionSelection, CartographerDemo, UserCodeUpload, UserCodeCartograph, CartographerLandingPage } from "./pages";

function App() {

    const [artifact, setArtifact] = useState(null);

    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="/landing-page" element={<CartographerLandingPage />} />

                <Route path="/" element={<DemoUploadChoice />} />
                <Route path="/demo-options" element={<DemoOptionSelection />} />
                <Route path="/cartographer-demo" element={<CartographerDemo />} />

                <Route path="/upload" element={<UserCodeUpload setArtifact={setArtifact} />} />
                <Route path="/cartograph" element={<UserCodeCartograph artifact={artifact} />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
