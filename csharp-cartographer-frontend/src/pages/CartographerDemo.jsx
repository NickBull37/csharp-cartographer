import { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import axios from "axios";
import { Box } from '@mui/material';
import { TitleBox, AiSidebar, TokenSidebar, CodeWindow } from "../components";

const CartographerDemo = () => {

    // Constants
    const [searchParams] = useSearchParams();
    const selectedFile = searchParams.get('file');

    // TitleBox State Variables
    const [artifactTitle, setArtifactTitle] = useState('');
    const [dateCreated, setDateCreated] = useState();
    const [artifactLanguage, setArtifactLanguage] = useState('');
    const [artifactType, setArtifactType] = useState('');
    const [numTokens, setNumTokens] = useState(0);
    const [numTags, setNumTags] = useState(0);
    const [numAncestors, setNumAncestors] = useState(0);
    const [generationTime, setGenerationTime] = useState('');

    // Left sidebar state variables
    const [artifactDescription, setArtifactDescription] = useState('');
    const [leftSidebarOpen, setLeftSidebarOpen] = useState(false);
    const [selectedTokens, setSelectedTokens] = useState([]);
    
    // Code content state variables
    const [navTokens, setNavTokens] = useState([]);
    const [activeHighlightIndices, setActiveHighlightIndices] = useState([]);

    // Right sidebar state variables
    const [activeToken, setActiveToken] = useState();
    
    // Event Handlers
    

    // Use Effects
    useEffect(() => {
        CreateArtifact(selectedFile);
    }, [selectedFile]);

    useEffect(() => {
        setActiveToken(navTokens[0]);
    }, [navTokens]);

    // API Calls
    async function CreateArtifact(selectedFile) {
        try {
            // Create artifact
            const response = await axios.get(`https://localhost:44300/Artifact/get-demo-artifact?fileName=${encodeURIComponent(selectedFile)}`);

            if (response.status === 200) {
                setArtifactTitle(response.data.title);
                setArtifactLanguage(response.data.language);
                setArtifactType(response.data.artifactType);
                setDateCreated(response.data.createdDate);
                setArtifactDescription(response.data.description);
                setNavTokens(response.data.navTokens);
                setNumTokens(response.data.numTokensAnalyzed);
                setNumTags(response.data.numLanguageElementTags);
                setNumAncestors(response.data.numAncestorsMapped);
                setGenerationTime(response.data.timeToGenerate);
            }
        } catch (error) {
            console.log('Create artifact API call failed.');
        }
    }

    return (
        <Box>

            <TitleBox
                artifactTitle={artifactTitle}
                leftSidebarOpen={leftSidebarOpen}
                numTokens={numTokens}
                numTags={numTags}
                numAncestors={numAncestors}
                generationTime={generationTime}
            />

            {/* <StructureDrawer
                leftSidebarOpen={leftSidebarOpen}
                setLeftSidebarOpen={setLeftSidebarOpen}
            /> */}

            <AiSidebar
                navTokens={navTokens}
                leftSidebarOpen={leftSidebarOpen}
                setLeftSidebarOpen={setLeftSidebarOpen}
                selectedTokens={selectedTokens}
                setSelectedTokens={setSelectedTokens}
            />

            <CodeWindow
                leftSidebarOpen={leftSidebarOpen}
                tokenList={navTokens}
                activeToken={activeToken}
                setActiveToken={setActiveToken}
                activeHighlightIndices={activeHighlightIndices}
                setActiveHighlightIndices={setActiveHighlightIndices}
                selectedTokens={selectedTokens}
                setSelectedTokens={setSelectedTokens}
            />

            <TokenSidebar
                navTokens={navTokens}
                activeToken={activeToken}
                setActiveToken={setActiveToken}
                activeHighlightIndices={activeHighlightIndices}
                setActiveHighlightIndices={setActiveHighlightIndices}
            />
            
        </Box>
    );
}
export default CartographerDemo;