import * as React from 'react';
import { useState } from 'react';
import { styled, useTheme } from '@mui/material/styles';
import { Box, Stack, Typography, Divider } from '@mui/material';
import { Drawer, IconButton } from '@mui/material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { TokenSidebarContent } from "../../../components";
import colors from '../../../utils/colors';
import { BorderColor } from '@mui/icons-material';

const HEADER_HEIGHT = 55;
const ARTIFACTBANNER_HEIGHT = 55;
const TOTAL_OFFSET = 110;

const CustomSidebar = styled(Drawer)(() => ({
    flexShrink: 0,
    
    '& .MuiDrawer-paper': {
        boxSizing: 'border-box',
        transition: 'width 0.3s',
        top: TOTAL_OFFSET, // set to 150px to create a gap below the existing AppBar & TitleBox
        height: `calc(100% - ${TOTAL_OFFSET}px)`, // adjust height to fit screen and allow scrolling
        // backgroundColor: colors.gray25,
        backgroundColor: colors.sidebarBg,
        borderColor: colors.gray50,
        color: '#fff',
        //borderRadius: '4px 0px 0px 0px',
        //boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
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
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    color: '#cccccc',
    position: 'absolute',
    top: '47%',
    left: '50%',
    transform: 'translate(-50%, -50%) rotate(-90deg)', // center and rotate text
    whiteSpace: 'nowrap',
    fontSize: '1.25rem',
    textShadow: '1px 1px 2px #000, 0 0 16px #333333, 0 0 5px #1a1a1a',
}));

const OpenSidebarHeaderText = styled(Typography)(() => ({
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    color: '#00e6cf',
}));

const RightSidebar = ({
    navTokens,
    activeToken,
    setActiveToken,
    activeHighlightIndices,
    setActiveHighlightIndices
}) => {

    // State Variables
    const [sidebarOpen, setSidebarOpen] = useState(false);

    // Event Handlers
    const handleDrawerToggle = () => {
        setSidebarOpen(!sidebarOpen);
    };

    return (

        <CustomSidebar
            variant="permanent"
            anchor="right"
            open={sidebarOpen}
            sx={{
                '& .MuiDrawer-paper': {
                    width: sidebarOpen
                        ? 450
                        : 55,
                    paddingBottom: '1rem',
                },
            }}
        >
            {!sidebarOpen
                ?
                    <Stack>
                        <ClosedSidebarToggle onClick={handleDrawerToggle}>
                            <KeyboardDoubleArrowLeftIcon />
                        </ClosedSidebarToggle>

                        <Divider sx={{ bgcolor: '#808080' }} />

                        <ClosedSidebarHeaderText>
                            Token Analysis
                        </ClosedSidebarHeaderText>
                    </Stack>
                :
                    <Stack>
                        <Box
                            display="flex"
                            //justifyContent="space-between"
                            gap={0.5}
                            alignItems="center"
                            sx={{
                                p: '1px 4px 1px 4px'
                            }}
                        >
                            {/* <OpenSidebarHeaderText>
                                Token Analysis
                            </OpenSidebarHeaderText> */}

                            <OpenSidebarToggle
                                onClick={handleDrawerToggle}
                                sx={{
                                    color: '#bfbfbf'
                                }}
                            >
                                <KeyboardDoubleArrowRightIcon />
                            </OpenSidebarToggle>
                            <Typography className='cartographer3'>
                                Token Analysis
                            </Typography>

                            
                        </Box>

                        <Divider sx={{ bgcolor: '#808080' }} />

                        <TokenSidebarContent
                            navTokens={navTokens}
                            activeToken={activeToken}
                            setActiveToken={setActiveToken}
                            activeHighlightIndices={activeHighlightIndices}
                            setActiveHighlightIndices={setActiveHighlightIndices}
                        />
                    </Stack>
            }
        </CustomSidebar>
    );
}

export default RightSidebar;