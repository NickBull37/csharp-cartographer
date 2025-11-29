import { useState, useEffect } from 'react';
import { Box } from '@mui/material';
import { TitleBox, StructureSidebar, TokenSidebar, CodeWindow } from "../components";

const UserCodeCartograph = ({artifact}) => {

    // TitleBox State Variables
    const [artifactTitle, setArtifactTitle] = useState('');
    const [artifactLanguage, setArtifactLanguage] = useState('');
    const [artifactType, setArtifactType] = useState('');
    const [dateCreated, setDateCreated] = useState();
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
    
    // Use Effects
    useEffect(() => {
        
        setArtifactTitle(artifact.title);
        setArtifactLanguage(artifact.language);
        setArtifactType(artifact.artifactType);
        setDateCreated(artifact.createdDate);
        setNavTokens(artifact.navTokens);

        console.log("Artifact", artifact);

    }, [artifact]);

    useEffect(() => {
        setActiveToken(navTokens[0]);
    }, [navTokens]);

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

            <StructureSidebar
                leftSidebarOpen={leftSidebarOpen}
                setLeftSidebarOpen={setLeftSidebarOpen}
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
                activeHighlightIndices={[]}
                setActiveHighlightIndices={setActiveHighlightIndices}
            />
            
        </Box>
    );
}
export default UserCodeCartograph;