import { useState, useEffect } from 'react';
import { Box } from '@mui/material';
//import { TitleBox, StructureSidebar, TokenSidebar, CodeWindow } from "../components";
import { ArtifactBanner, LeftSidebar, RightSidebar, CodeWindow } from "../components";

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
    const [activeHighlightRange, setActiveHighlightRange] = useState(null);

    // Right sidebar state variables
    const [activeToken, setActiveToken] = useState();
    
    // Use Effects
    useEffect(() => {
        
        setArtifactTitle(artifact.title);
        setArtifactLanguage(artifact.language);
        setArtifactType(artifact.artifactType);
        setDateCreated(artifact.createdDate);
        setNavTokens(artifact.navTokens);
        setNumTokens(artifact.numTokensAnalyzed);
        setNumTags(artifact.numLanguageElementTags);
        setNumAncestors(artifact.numAncestorsMapped);
        setGenerationTime(artifact.timeToGenerate);

        console.log("Artifact", artifact);

    }, [artifact]);

    useEffect(() => {
        setActiveToken(navTokens[0]);
    }, [navTokens]);

    return (
        <Box>

            <ArtifactBanner
                artifactTitle={artifactTitle}
                leftSidebarOpen={leftSidebarOpen}
                numTokens={numTokens}
                numTags={numTags}
                numAncestors={numAncestors}
                generationTime={generationTime}
            />

            <LeftSidebar
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
                activeHighlightRange={activeHighlightRange}
                setActiveHighlightRange={setActiveHighlightRange}
                selectedTokens={selectedTokens}
                setSelectedTokens={setSelectedTokens}
            />

            <RightSidebar
                navTokens={navTokens}
                activeToken={activeToken}
                setActiveToken={setActiveToken}
                activeHighlightRange={activeHighlightRange}
                setActiveHighlightRange={setActiveHighlightRange}
            />
            
        </Box>
    );
}
export default UserCodeCartograph;