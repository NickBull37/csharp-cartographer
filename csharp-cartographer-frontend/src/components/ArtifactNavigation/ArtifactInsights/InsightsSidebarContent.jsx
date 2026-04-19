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
    letterSpacing: '0.05em',
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
    [`& .${accordionSummaryClasses.expandIconWrapper}.${accordionSummaryClasses.expanded}`]:
        {
            transform: 'rotate(90deg)',
        },
}));

const AccordionDetails = styled(MuiAccordionDetails)(({ theme }) => ({
    padding: '20px',
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
    artifactInsight,
    selectedTokens,
    setSelectedTokens
}) => {

    // State Variables
    const [expanded, setExpanded] = useState(artifactInsight.notes[0].id);
    const [activeNote, setActiveNote] = useState(artifactInsight.notes[0]);

    // Event Handlers
    const handleAccordianChange = (noteId) => (event, newExpanded) => {
        setExpanded(newExpanded ? noteId : false);

        if (!newExpanded) {
            setSelectedTokens([]);
            setActiveNote(null);
            return;
        }

        const matchingNote = artifactInsight.notes.find(note => note.id === noteId);
        setActiveNote(matchingNote ?? null);
        setSelectedTokens(matchingNote?.highlights ?? []);
    };

    function HandleClearTokens() {
        setSelectedTokens([]);
    }

    // Use Effects

    // API Calls

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
                            px: '20px',
                            py: '10px',
                        }}
                    >
                        <FileNameLabel>
                            {artifactInsight.label}
                        </FileNameLabel>
                        <InsightLabel>
                            Insight
                        </InsightLabel>
                    </Box>
                    <Box
                        sx={{
                            background: 'rgba(38, 38, 38, 0.375)',
                            p: '20px',
                        }}
                    >
                        <InsightDescription>
                            {artifactInsight.description}
                        </InsightDescription>
                    </Box>
                </Box>

                <div>
                    {artifactInsight.notes.map((note, index) => (
                        <Accordion
                            key={note.id}
                            expanded={expanded === note.id}
                            onChange={handleAccordianChange(note.id)}
                        >
                            <AccordionSummary
                                aria-controls={`${note.id}-content`}
                                id={`${note.id}-header`}
                            >
                                <Box
                                    display="flex"
                                    alignItems="center"
                                >
                                    <ExploreIcon
                                        sx={{
                                            fontSize: '19px',
                                            color: note.id === activeNote?.id
                                                ? '#ff66b3'
                                                : colors.gray45,
                                            mr: '0.75rem',
                                        }}
                                    />
                                    <InsightNoteLabel component="span">
                                        {note.label}
                                    </InsightNoteLabel>
                                </Box>
                            </AccordionSummary>
                            <AccordionDetails>
                                <InsightNoteText>
                                    {note.text}
                                </InsightNoteText>
                            </AccordionDetails>
                        </Accordion>
                    ))}
                </div>

                <Box
                    display="flex"
                    justifyContent="center"
                    sx={{
                        mt: '2rem'
                    }}
                >
                    <ClearBtn
                        variant="outlined"
                        size="small"
                        onClick={HandleClearTokens}
                    >
                        Clear Tokens
                    </ClearBtn>
                </Box>
            </Stack>

        </ContentContainer>
    );
}

export default InsightsSidebarContent;