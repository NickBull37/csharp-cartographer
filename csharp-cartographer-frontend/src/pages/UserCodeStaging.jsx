import React, { useState, useEffect } from 'react';
import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Stack, Paper, Typography, Divider } from '@mui/material';
import { Drawer, AppBar, Toolbar, IconButton, List, ListItem, ListItemIcon, ListItemText, Collapse } from '@mui/material';
import { Menu as MenuIcon, ExpandLess, ExpandMore, Inbox as InboxIcon, Mail as MailIcon } from '@mui/icons-material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { StagingTitleBox, StagingCodeContent, StagingRightSideBar, StagingLeftSideBar } from "../components";

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const PageContainer = styled(Box)(() => ({

}));

const UserCodeStaging = ({ artifact, setArtifact}) => {

    // Constants

    // State Variables
    const [artifactTemplateTitle, setArtifactTemplateTitle] = useState('');
    const [artifactTemplateLanguage, setArtifactTemplateLanguage] = useState('');
    const [artifactTemplateType, setArtifactTemplateType] = useState();
    const [artifactTemplateDescription, setArtifactTemplateDescription] = useState('');
    const [dateCreated, setDateCreated] = useState();
    const [navTokens, setNavTokens] = useState([]);
    
    const [activeToken, setActiveToken] = useState();
    const [leftSideBarOpen, setLeftSideBarOpen] = useState(false);
    
    // pass to right-sidebar
    const [selectedTokenIndexList, setSelectedTokenIndexList] = useState([]);

    const [insightHighlightIndexes, setInsightHighlightIndexes] = useState([]);

    // Event Handlers
    

    // Use Effects
    useEffect(() => {

        setArtifactTemplateTitle(artifact.title);
        setArtifactTemplateLanguage(artifact.language);
        //setArtifactType(artifact.type);
        setDateCreated(artifact.dateCreated);
        //setArtifactDescription(artifact.description);
        setNavTokens(artifact.navTokens);

    }, [artifact]);

    useEffect(() => {
        setActiveToken(navTokens[0]);
    }, [navTokens]);

    // API Calls
    async function SaveArtifact() {
        try {
            // Create artifact
            const response = await axios.get("https://localhost:44300/Artifact/create-artifact");

            if (response.status === 200) {

            }
        } catch (error) {
            console.log('Create artifact API call failed.');
        }
    }

    return (
        <Box sx={{ height: '2000px'}}>

            <StagingTitleBox
                artifactTemplateTitle={artifactTemplateTitle}
                setArtifactTemplateTitle={setArtifactTemplateTitle}
                artifactTemplateType={artifactTemplateType}
                setArtifactTemplateType={setArtifactTemplateType}
                setArtifactTemplateDescription={setArtifactTemplateDescription}
                navTokens={navTokens}
            />

            <StagingLeftSideBar
                leftSideBarOpen={leftSideBarOpen}
                setLeftSideBarOpen={setLeftSideBarOpen}
                artifactTemplateDescription={artifactTemplateDescription}
                setArtifactTemplateDescription={setArtifactTemplateDescription}
            />

            {/* Code area */}
            <StagingCodeContent
                navTokens={navTokens}
                selectedTokenIndexList={selectedTokenIndexList}
                setSelectedTokenIndexList={setSelectedTokenIndexList}
                leftSideBarOpen={leftSideBarOpen}
                // insightHighlightIndexes={insightHighlightIndexes}
                // setInsightHighlightIndexes={setInsightHighlightIndexes}
            />

            {/* Left side bar */}
            {/* <StagingRightSideBar
                selectedTokenIndexList={selectedTokenIndexList}
                setSelectedTokenIndexList={setSelectedTokenIndexList}
            /> */}
        </Box>
    );
}
export default UserCodeStaging;