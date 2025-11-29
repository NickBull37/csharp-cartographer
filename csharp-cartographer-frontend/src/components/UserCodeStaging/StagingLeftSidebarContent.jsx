import React, { useState, useEffect } from 'react';
import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Stack, Paper, Typography, Divider, TextField } from '@mui/material';
import { Drawer, AppBar, Toolbar, IconButton, List, ListItem, ListItemIcon, ListItemText, Collapse } from '@mui/material';
import { Menu as MenuIcon, ExpandLess, ExpandMore, Inbox as InboxIcon, Mail as MailIcon } from '@mui/icons-material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { StructureContent } from "..";
import colors from '../../utils/colors';

const TitleText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '18px',
    color: '#00FFC7',
    marginBottom: '4px'
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
    padding: '8px',
    height: '82%',
}));

const StagingLeftSidebarContent = ({artifactTemplateDescription, setArtifactTemplateDescription}) => {
    
    const handleArtifactDescriptionChange = (event) => {
        setArtifactTemplateDescription(event.target.value);
    };

    return (
        <Stack
            sx={{
                padding: '10px',
            }}
        >
            <TitleText>
                Description
            </TitleText>
            <TextField
                fullWidth
                id="artifact-desc"
                label="Add artifact description"
                multiline
                rows={18}
                defaultValue=""
                variant="filled"
                onChange={handleArtifactDescriptionChange}
                sx={{
                    borderRadius: '4px',
                    '& .MuiFilledInput-root': {
                            color: '#fff',
                            backgroundColor: '#666666',
                        },
                    '& .MuiInputLabel-root': {
                        color: '#cccccc',
                    },
                    '& .MuiInputLabel-root.Mui-focused': {
                        color: '#cccccc',
                    },
                    '& .MuiFilledInput-root:hover': {
                        backgroundColor: '#737373',
                    },
                    '& .MuiFilledInput-root.Mui-focused': {
                        backgroundColor: '#666666',
                    }
                }}
            />
        </Stack>
    );
}

export default StagingLeftSidebarContent;