import * as React from 'react';
import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import axios from "axios";
import { Box, Stack, Typography, TextField } from '@mui/material';
import { ToggleButton, Divider, Button } from '@mui/material';
import SendIcon from '@mui/icons-material/Send';
import colors from '../../../utils/colors';
import InfoOutlineIcon from '@mui/icons-material/InfoOutline';
import ArrowForwardIosSharpIcon from '@mui/icons-material/ArrowForwardIosSharp';
import ExploreIcon from '@mui/icons-material/Explore';
import MuiAccordion from '@mui/material/Accordion';
import MuiAccordionSummary, {
  accordionSummaryClasses,
} from '@mui/material/AccordionSummary';
import MuiAccordionDetails from '@mui/material/AccordionDetails';

const FileNameLabel = styled(Typography)(() => ({
    fontSize: '14px',
    color: colors.gray95,
    textTransform: 'uppercase',
    letterSpacing: '0.035em',
    fontFamily: 'Segoe UI, Segoe UI Variable Text, -apple-system, BlinkMacSystemFont, Helvetica Neue, Helvetica, Arial, sans-serif',
    fontWeight: '600'
}));
const InsightLabel = styled(Typography)(() => ({
    fontSize: '14px',
    color: 'rgba(255, 128, 191, 1)',
    textTransform: 'uppercase',
    letterSpacing: '0.035em',
    fontFamily: 'Segoe UI, Segoe UI Variable Text, -apple-system, BlinkMacSystemFont, Helvetica Neue, Helvetica, Arial, sans-serif',
    fontWeight: '600'
}));
const InsightDescription = styled(Typography)(() => ({
    fontFamily: 'Segoe UI, Segoe UI Variable Text, -apple-system, BlinkMacSystemFont, Helvetica Neue, Helvetica, Arial, sans-serif',
    fontSize: '14px',
    letterSpacing: '0.04em',
    color: colors.gray95
}));
const InsightNoteLabel = styled(Typography)(() => ({
    fontSize: '13px',
    color: colors.gray95,
    textTransform: 'uppercase',
    letterSpacing: '0.035em',
    fontFamily: 'Segoe UI, Segoe UI Variable Text, -apple-system, BlinkMacSystemFont, Helvetica Neue, Helvetica, Arial, sans-serif',
    fontWeight: '600'
}));
const InsightNoteText = styled(Typography)(() => ({
    fontFamily: 'Segoe UI, Segoe UI Variable Text, -apple-system, BlinkMacSystemFont, Helvetica Neue, Helvetica, Arial, sans-serif',
    fontSize: '14px',
    letterSpacing: '0.04em',
    color: colors.white,
    borderRadius: '4px',
    //boxShadow: '0 1px 4px 0 rgba(0, 0, 0, 0.45), 0 2px 6px 0 rgba(0, 0, 0, 0.45)',
}));

const Accordion = styled((props) => (
    <MuiAccordion disableGutters elevation={0} square {...props} />
))(() => ({
    background: colors.sidebarBg,
}));

const AccordionSummary = styled((props) => (
    <MuiAccordionSummary
        expandIcon={<ArrowForwardIosSharpIcon sx={{ fontSize: '0.9rem', color: '#fff' }} />}
        {...props}
    />
))(() => ({
    background: 'rgba(38, 38, 38, 0.75)',
    margin: 0,
    paddingTop: 0,
    paddingBottom: 0,
    [`& .${accordionSummaryClasses.expandIconWrapper}.${accordionSummaryClasses.expanded}`]:
        {
            transform: 'rotate(90deg)',
        },
}));

const AccordionDetails = styled(MuiAccordionDetails)(({ theme }) => ({
    padding: '18px',
    background: 'rgba(38, 38, 38, 0.375)',
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
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

    const [expanded, setExpanded] = React.useState('panel1');

    // Event Handlers
    const handleAccordianChange = (panel) => (event, newExpanded) => {
        setExpanded(newExpanded ? panel : false);
    };

    function HandleClearTokens() {
        setSelectedTokens([]);
    }

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

    return (
        <ContentContainer>
            <Stack>
                <Box>
                    <Box
                        display="flex"
                        justifyContent="space-between"
                        alignItems="center"
                        sx={{
                            background: 'rgba(38, 38, 38, 0.75)',
                            px: '14px',
                            py: '10px',
                        }}
                    >
                        <FileNameLabel>
                            Data Transfer Object
                        </FileNameLabel>
                        <InsightLabel>
                            Insight
                        </InsightLabel>
                    </Box>
                    <Box
                        sx={{
                            background: 'rgba(38, 38, 38, 0.375)',
                            p: '18px',
                        }}
                    >
                        <InsightDescription>
                            This dto was designed to be immutable once received to avoid any unintentional changes in the data that could cause downstream side effects. It also has built in validation to ensure all required data is provided.
                        </InsightDescription>
                    </Box>
                </Box>

                <div>
                    <Accordion
                        expanded={expanded === 'panel1'}
                        onChange={handleAccordianChange('panel1')}
                    >
                        <AccordionSummary
                            aria-controls="panel1d-content"
                            id="panel1d-header"
                        >
                            <Box
                                display="flex"
                                alignItems="center"
                            >
                                <ExploreIcon
                                    sx={{
                                        fontSize: '19px',
                                        color: 'rgba(255, 128, 191, 1)',
                                        mr: '0.75rem',
                                    }}
                                />
                                <InsightNoteLabel component="span">Insight #1</InsightNoteLabel>
                            </Box>
                        </AccordionSummary>
                        <AccordionDetails>
                            <InsightNoteText>
                                The "Required" attribute ensures a value is provided by the incoming request and automatically returns a 400 Bad Request error if a value is missing.
                            </InsightNoteText>
                        </AccordionDetails>
                    </Accordion>
                    <Accordion expanded={expanded === 'panel2'} onChange={handleAccordianChange('panel2')}>
                        <AccordionSummary aria-controls="panel2d-content" id="panel2d-header">
                            <Box
                                display="flex"
                                alignItems="center"
                            >
                                <ExploreIcon
                                    sx={{
                                        fontSize: '19px',
                                        color: colors.gray45,
                                        mr: '0.75rem',
                                    }}
                                />
                                <InsightNoteLabel component="span">Insight #2</InsightNoteLabel>
                            </Box>
                        </AccordionSummary>
                        <AccordionDetails>
                            <InsightNoteText>
                                This attribute specifies the value used for this property during json deserialization and ensures the right assignment is made, although these names would match by default so this isn't totally necessary.
                            </InsightNoteText>
                        </AccordionDetails>
                    </Accordion>
                    <Accordion expanded={expanded === 'panel3'} onChange={handleAccordianChange('panel3')}>
                        <AccordionSummary aria-controls="panel3d-content" id="panel3d-header">
                            <Box
                                display="flex"
                                alignItems="center"
                            >
                                <ExploreIcon
                                    sx={{
                                        fontSize: '19px',
                                        color: colors.gray45,
                                        mr: '0.75rem',
                                    }}
                                />
                                <InsightNoteLabel component="span">Insight #3</InsightNoteLabel>
                            </Box>
                        </AccordionSummary>
                        <AccordionDetails>
                            <InsightNoteText>
                                Dto properties are init-only and can only be assigned a value at object creation. This prevents any unintentional modification of data that has no reason to ever change.
                            </InsightNoteText>
                        </AccordionDetails>
                    </Accordion>
                    <Accordion expanded={expanded === 'panel4'} onChange={handleAccordianChange('panel4')}>
                        <AccordionSummary aria-controls="panel4d-content" id="panel4d-header">
                            <Box
                                display="flex"
                                alignItems="center"
                            >
                                <ExploreIcon
                                    sx={{
                                        fontSize: '19px',
                                        color: colors.gray45,
                                        mr: '0.75rem',
                                    }}
                                />
                                <InsightNoteLabel component="span">Insight #4</InsightNoteLabel>
                            </Box>
                        </AccordionSummary>
                        <AccordionDetails>
                            <InsightNoteText>
                                This assignment to default just provides a valid syntax to apply the null-forgiving operator to the property. This removes nullability warnings safely since a value is required on the incoming request.
                            </InsightNoteText>
                        </AccordionDetails>
                    </Accordion>
                </div>
            </Stack>





























            {/*---------------- Save Insight Tab ----------------*/}
            {/* <Stack
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
            </Stack> */}
        </ContentContainer>
    );
}

export default InsightsSidebarContent;