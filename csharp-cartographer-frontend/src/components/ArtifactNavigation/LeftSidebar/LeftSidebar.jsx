import { useState } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography, Divider } from '@mui/material';
import { Drawer, IconButton } from '@mui/material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { AiSidebarContent, InsightsSidebarContent } from "../../../components";
import colors from '../../../utils/colors';

const HEADER_HEIGHT = 55;
const ARTIFACTBANNER_HEIGHT = 55;
const TOTAL_OFFSET = 110;

const CustomSidebar = styled(Drawer)(() => ({
    flexShrink: 0,
    '& .MuiDrawer-paper': {
        boxSizing: 'border-box',
        transition: 'width 0.3s',
        top: TOTAL_OFFSET, // set to 110px to create a gap below the existing AppBar & TitleBox
        height: `calc(100% - ${TOTAL_OFFSET}px)`, // adjust height to fit screen and allow scrolling
        backgroundColor: colors.sidebarBg,
        borderColor: colors.gray50,
        color: '#fff',
        overflow: 'auto',
        '&::-webkit-scrollbar': {
            width: '4px', // width of the scrollbar
        },
        '&::-webkit-scrollbar-track': {
            backgroundColor: '#333333', // track color
            borderRadius: '10px', // rounded corners for the track
        },
        '&::-webkit-scrollbar-thumb': {
            backgroundColor: '#555555', // thumb color
            borderRadius: '10px', // rounded corners for the thumb
            '&:hover': {
                backgroundColor: '#888888', // thumb color on hover
            },
        },
    },
}));

const ClosedSidebarToggle = styled(IconButton)(() => ({
    color: '#cccccc',
    borderRadius: '0',
    transition: 'background-color 200ms ease, color 200ms ease',
    '&:hover': {
        backgroundColor: 'rgba(0, 0, 0, 0.15)',
        borderRadius: '0',
        transition: 'background-color 200ms ease, color 200ms ease',
    },
}));

const OpenSidebarToggle = styled(IconButton)(() => ({
    color: '#cccccc',
    transition: 'background-color 200ms ease, color 200ms ease',
    '&:hover': {
        backgroundColor: 'rgba(0, 0, 0, 0.15)',
        transition: 'background-color 200ms ease, color 200ms ease',
    },
}));

const ClosedSidebarHeaderText = styled(Typography)(() => ({
    color: '#cccccc',
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -95%)',
    whiteSpace: 'nowrap',
}));

const LeftSidebar = ({
    navTokens,
    leftSidebarOpen,
    setLeftSidebarOpen,
    selectedTokens,
    setSelectedTokens
}) => {

    // Event Handlers
    const handleDrawerToggle = () => {
        setLeftSidebarOpen(!leftSidebarOpen);
    };

    const VerticalText = ({ text }) => (
        <Stack alignItems="center" spacing={0}>
            {text.split('').map((char, i) => (
                <span key={i}>
                    {char === ' ' ? '\u00A0' : char}
                </span>
            ))}
        </Stack>
    );

    return (
        <CustomSidebar
            variant="permanent"
            anchor="left"
            open={leftSidebarOpen}
            sx={{
                '& .MuiDrawer-paper': {
                    width: leftSidebarOpen
                        ? 385
                        : 55,
                },
            }}
        >
            {!leftSidebarOpen
                ?
                    <Stack>
                        <ClosedSidebarToggle onClick={handleDrawerToggle}>
                            <KeyboardDoubleArrowRightIcon />
                        </ClosedSidebarToggle>

                        <Divider sx={{ bgcolor: '#808080' }} />

                        <ClosedSidebarHeaderText className='cartographer4'>
                            <VerticalText text="Artifact Legend" />
                        </ClosedSidebarHeaderText>
                    </Stack>
                :
                    <Stack>
                        <Box
                            display="flex"
                            justifyContent="space-between"
                            alignItems="center"
                            sx={{
                                p: '4px 4px 4px 12px'
                            }}
                        >
                            <Typography className='cartographer3'>
                                Artifact Legend
                            </Typography>

                            <OpenSidebarToggle
                                onClick={handleDrawerToggle}
                                sx={{
                                    color: '#cccccc'
                                }}
                            >
                                <KeyboardDoubleArrowLeftIcon />
                            </OpenSidebarToggle>
                        </Box>

                        <Divider sx={{ bgcolor: '#808080' }} />

                        {/* <AiSidebarContent
                            navTokens={navTokens}
                            selectedTokens={selectedTokens}
                            setSelectedTokens={setSelectedTokens}
                        /> */}
                        <InsightsSidebarContent
                            navTokens={navTokens}
                            selectedTokens={selectedTokens}
                            setSelectedTokens={setSelectedTokens}
                        />
                    </Stack>
            }
        </CustomSidebar>
    );
}

export default LeftSidebar;