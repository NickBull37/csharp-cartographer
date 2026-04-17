import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import axios from "axios";
import { Box, Stack, Typography, TextField } from '@mui/material';
import { ToggleButton, Divider, Button } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import colors from '../../../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
    paddingRight: '20px',
    paddingLeft: '20px',
    paddingTop: '22px',
    height: '100%',
}));

const ClearBtn = styled(Button)(() => ({
    height: '25px',
    width: '140px',
    color: '#fff',
    borderColor: '#ff4da6',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.26), 0px 4px 5px 0px rgba(0, 0, 0, 0.18), 0px 1px 10px 0px rgba(0, 0, 0, 0.14)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        borderColor: '#ff66b3',
        background: 'rgba(255, 77, 166, 0.1)',
        boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    },
}));

const SaveBtn = styled(Button)(() => ({
    height: '25px',
    width: '140px',
    color: '#fff',
    backgroundColor: 'rgba(255, 51, 153, 0.7)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.26), 0px 4px 5px 0px rgba(0, 0, 0, 0.18), 0px 1px 10px 0px rgba(0, 0, 0, 0.14)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        backgroundColor: 'rgba(255, 0, 128, 0.7)',
        boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    },
}));

const StyledTextField = styled(TextField)(() => ({
    width: '100%',
    '& .MuiInputLabel-root': {
        fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
        fontSize: '14px',
        letterSpacing: '0.04em',
        color: colors.gray80,
    },
    '& .MuiInputLabel-root.Mui-focused': {
        color: colors.gray85,
    },
    '& .MuiOutlinedInput-root': {
        fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
        fontSize: '14px',
        letterSpacing: '0.04em',
        backgroundColor: colors.gray20,
        '& fieldset': {
            border: '1px solid',
            borderColor: colors.divider,
            borderRadius: '4px',
        },
        '&:hover fieldset': {
            borderColor: "#999999",
        },
        '&.Mui-focused fieldset': {
            borderColor: "#999999",
        },
    },
    '& .MuiOutlinedInput-input': {
        padding: '8px',
        color: '#fff',
    },
    '& .MuiOutlinedInput-input::placeholder': {
        color: colors.gray95,
    },
}));

const InsightsSidebarContent = ({
    navTokens,
    selectedTokens,
    setSelectedTokens
}) => {

    // State Variables
    const [artifactId, setArtifactId] = useState();
    const [insightText, setInsightText] = useState();
    const [insightTokens, setInsightTokens] = useState([]);

    // Use Effects
    useEffect(() => {
        setInsightTokens(selectedTokens);
    }, [selectedTokens]);

    // API Calls
    async function SaveInsight() {
        try {
            const response = await axios.post("https://localhost:44300/Artifact/save-insight", {
                artifactId: artifactId,
                insightText: insightText,
                insightTokens: insightTokens
            });

            if (response.status === 200) {

            }
        } catch (error) {
            console.log(error);
        } finally {

        }
    }

    // Event Handlers
    function HandleClearTokens() {
        setSelectedTokens([]);
    }

    return (
        <ContentContainer>
            <Stack
                gap={3}
                sx={{
                    width: '100%'
                }}
            >
                <FlexBox
                    justifyContent="space-between"
                >
                    <ClearBtn
                        variant="outlined"
                        size="small"
                        onClick={HandleClearTokens}
                    >
                        Clear Tokens
                    </ClearBtn>
                    <SaveBtn
                        variant="contained"
                        size="small"
                        endIcon={
                            <SendIcon
                                sx={{
                                    color: '#fff',
                                    height: '14px',
                                    width: '14px',
                                    mt: '-2px'
                                }}
                            />
                        }
                    >
                        Save Insight
                    </SaveBtn>
                </FlexBox>

                <StyledTextField
                    label="Description"
                    variant="outlined"
                    multiline
                    // rows={30}
                    rows={10}
                    placeholder="Enter description..."
                />
            </Stack>
        </ContentContainer>
    );
}

export default InsightsSidebarContent;