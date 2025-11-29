import React, { useState, useEffect } from 'react';
import axios from "axios";
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Stack, Paper, Typography, TextField, Tooltip, Button } from '@mui/material';
import DoubleArrowIcon from '@mui/icons-material/DoubleArrow';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import ApiIcon from '@mui/icons-material/Api';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import CodeIcon from '@mui/icons-material/Code';
import DataArrayIcon from '@mui/icons-material/DataArray';
import DataObjectIcon from '@mui/icons-material/DataObject';

const SideBarPaper = styled(Paper)(() => ({
    backgroundColor: '#4d4d4d',
    boxShadow: "0px 2px 10px 0.6px #1a1a1a",
    padding: "12px 16px",
    color: "#fff",
    height: "525px",
    width: "350px",
    position: "fixed",
    right: 30,
    top: 155,
    overflow: 'auto',
    /* Scrollbar Styles for WebKit browsers (Chrome, Safari) */
    '::-webkit-scrollbar': {
        width: '3px',
    },
    '::-webkit-scrollbar-track': {
        background: '#4d4d4d', /* Background of the track */
        borderRadius: '4px',
    },
    '::-webkit-scrollbar-thumb': {
        background: '#00998a', /* Color of the thumb (scroll handle) */
        borderRadius: '4px',
    },
    '::-webkit-scrollbar-thumb:hover': {
        background: '#00b3a1', /* Hover color of the thumb */
    },
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'baseline',
    marginTop: '16px',
}));

const InsightIconBox = styled(Box)(() => ({
    display: 'flex',
    padding: '6px 6px',
    backgroundColor: '#666666',
    borderRadius: '3px',
}));

const SaveInsightBtn = styled(Button)(() => ({
    color: '#fff',
    background: 'linear-gradient(to right, #990073, #ff1ac6)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.2), 0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12)',
    marginTop: '4px',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #800060, #ff00bf)',
    },
}));

const ClearTokensBtn = styled(Button)(() => ({
    color: '#ff33cc',
    border: '1px solid #ff33cc',
    backgroundColor: 'rgba(255, 51, 204, 0.05)',
    marginBottom: '16px',
    padding: '2px 8px 0px 8px',
    '&:hover': {
        color: '#ff00bf',
        border: '1px solid #ff00bf',
        backgroundColor: 'rgba(255, 51, 204, 0.1)',
    },
}));

const OrangeBox = styled(Box)(() => ({
    padding: '4px 8px',
    border: '1px solid rgba(255, 136, 77, 1)',
    borderRadius: '4px',
    backgroundColor: 'rgba(255, 136, 77, 0.1)'
}));

const TokenText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '16px',
    borderRadius: '4px',
    boxShadow: '0px 2px 4px 0.4px #333333',
    marginTop: '8px',
    padding: '4px 12px',
}));

const BoxTitle = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '18px',
    color: '#00FFC7',
}));

const BoxText = styled(Typography)(() => ({
    fontFamily: 'Mona Sans, MonaSansFallback, Segoe UI, Helvetica, Arial, sans-serif',
    fontSize: '14px',
}));

const StagingRightSidebar = ({ selectedTokenIndexList, setSelectedTokenIndexList }) => {

    // State Variables
    const [devInsightTitle, setDevInsightTitle] = useState('');
    const [devInsightTokenList, setDevInsightTokenList] = useState('');
    const [devInsightContent, setDevInsightContent] = useState('');

    const [savedInsights, setSavedInsights] = useState([]);

    // Event Handlers
    const handleClearTokens = () => {
        setSelectedTokenIndexList([]);
    };

    const handleInsightContentChange = (event) => {
        setDevInsightContent(event.target.value);
    };

    // Use Effects
    useEffect(() => {
        
    }, []);

    // API Calls
    async function SaveArtifact() {
        try {
            // Save artifact to database
            const response = await axios.post("https://localhost:44300/Insight/get-comprehensive-insight", {
                devInsightTitle: devInsightTitle,
            });

            if (response.status === 200) {

            }
        } catch (error) {
            alert(error);
        }
    }

    return (
        <SideBarPaper>
            <Stack height={'100%'} gap={2.5}>
                <Stack>
                    <Box
                        display="flex"
                        justifyContent="space-between"
                        alignItems="flex-end"
                    >
                        <BoxTitle>
                            Saved Insights
                        </BoxTitle>
                        <ClearTokensBtn
                            variant='outlined'
                            size='small'
                            onClick={handleClearTokens}
                        >
                            Clear Tokens
                        </ClearTokensBtn>
                    </Box>
                    <InsightIconBox>
                        <ApiIcon fontSize='small' sx={{ color: '#ff4dd2' }} />
                    </InsightIconBox>
                </Stack>
                <Stack>
                    <BoxTitle>
                        New Insight
                    </BoxTitle>
                    <TextField
                        fullWidth
                        id="dev-insight"
                        label="Add developer insight for highlighted tokens"
                        multiline
                        rows={12}
                        defaultValue=""
                        variant="filled"
                        onChange={handleInsightContentChange}
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
                <SaveInsightBtn
                    variant='contained'
                    size='small'
                    endIcon={
                        <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-floppy" viewBox="0 0 16 16">
                            <path d="M11 2H9v3h2z"/>
                            <path d="M1.5 0h11.586a1.5 1.5 0 0 1 1.06.44l1.415 1.414A1.5 1.5 0 0 1 16 2.914V14.5a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 14.5v-13A1.5 1.5 0 0 1 1.5 0M1 1.5v13a.5.5 0 0 0 .5.5H2v-4.5A1.5 1.5 0 0 1 3.5 9h9a1.5 1.5 0 0 1 1.5 1.5V15h.5a.5.5 0 0 0 .5-.5V2.914a.5.5 0 0 0-.146-.353l-1.415-1.415A.5.5 0 0 0 13.086 1H13v4.5A1.5 1.5 0 0 1 11.5 7h-7A1.5 1.5 0 0 1 3 5.5V1H1.5a.5.5 0 0 0-.5.5m3 4a.5.5 0 0 0 .5.5h7a.5.5 0 0 0 .5-.5V1H4zM3 15h10v-4.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5z"/>
                        </svg>
                    }
                >
                    Insight
                </SaveInsightBtn>
            </Stack>
        </SideBarPaper>
    );
}

export default StagingRightSidebar;