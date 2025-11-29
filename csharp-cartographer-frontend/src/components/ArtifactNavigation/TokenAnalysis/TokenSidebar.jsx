import React, { useState, useEffect } from 'react';
import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography, Divider } from '@mui/material';
import { Drawer, IconButton, List } from '@mui/material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import BlurCircularIcon from '@mui/icons-material/BlurCircular';
import { TokenSidebarContent } from "../../../components";
import colors from '../../../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const CustomDrawer = styled(Drawer)(() => ({
    '& .MuiDrawer-paper': {
        '&::-webkit-scrollbar': {
            width: '4px', // Width of the scrollbar
        },
        '&::-webkit-scrollbar-track': {
            backgroundColor: '#333333', // Track color
            borderRadius: '10px', // Rounded corners for the track
        },
        '&::-webkit-scrollbar-thumb': {
            backgroundColor: '#555555', // Thumb color
            borderRadius: '10px', // Rounded corners for the thumb
            '&:hover': {
                backgroundColor: '#888888', // Thumb color on hover
            },
        },
    },
}));

const TokenSidebar = ({ navTokens, activeToken, setActiveToken, activeHighlightIndices, setActiveHighlightIndices }) => {

    const [drawerOpen, setDrawerOpen] = useState(false);

    const handleDrawerToggle = () => {
        setDrawerOpen(!drawerOpen);
    };

    return (
        <div>
            {/* Drawer (Sidebar) */}
            <CustomDrawer
                variant="permanent"
                anchor="right"
                open={drawerOpen}
                sx={{
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerOpen ? 390 : 50, // Full width when open, 50px when collapsed
                        boxSizing: 'border-box',
                        transition: 'width 0.3s',
                        top: '140px', // Set to 150px to create a gap below the existing AppBar & TitleBox
                        height: 'calc(100% - 140px)', // Adjust height to fit screen and allow scrolling
                        backgroundColor: colors.gray25,
                        color: '#fff',
                        borderRadius: '4px 0px 0px 0px', // Rounded right edge
                        boxShadow: '0px 3px 5px -1px rgba(0, 0, 0, 0.6), 0px 5px 6px 0px rgba(0, 0, 0, 0.36), 1px 2px 11px 0px rgba(0, 0, 0, 0.32)',
                        overflow: 'auto'
                    },
                }}
            >
                {drawerOpen
                    ?
                        <FlexBox
                            justifyContent="space-between"
                            alignItems="center"
                            sx={{
                                p: 0.5
                            }}
                        >
                            <Typography
                                className='code color-code'
                                sx={{
                                    ml: 2.5,
                                }}
                            >
                                Token Analysis
                            </Typography>
                            <IconButton
                                onClick={handleDrawerToggle}
                                sx={{
                                    color: '#cccccc'
                                }}
                            >
                                <KeyboardDoubleArrowRightIcon />
                            </IconButton>
                        </FlexBox>
                    :
                        <IconButton
                            onClick={handleDrawerToggle}
                            sx={{
                                marginTop: 1,
                                marginLeft: 'auto',
                                marginRight: 'auto',
                                color: '#cccccc'
                            }}
                        >
                            <KeyboardDoubleArrowLeftIcon />
                        </IconButton>
                }
                <Divider sx={{ bgcolor: '#808080' }} />
                {!drawerOpen
                    ?
                        <Box
                            display="flex"
                            justifyContent="center"
                        >
                            <Typography
                                className='code'
                                sx={{
                                    color: '#cccccc',
                                    position: 'absolute',
                                    top: '47%',
                                    left: '50%',
                                    transform: 'translate(-50%, -50%) rotate(-90deg)', // Center and rotate text
                                    whiteSpace: 'nowrap',
                                    fontSize: '1.25rem',
                                    textShadow: '1px 1px 2px #000, 0 0 16px #333333, 0 0 5px #1a1a1a',
                                }}
                                >
                                Token Analysis
                            </Typography>
                            {/* <BlurCircularIcon

                            /> */}
                        </Box>
                    :
                        <TokenSidebarContent
                            navTokens={navTokens}
                            activeToken={activeToken}
                            setActiveToken={setActiveToken}
                            activeHighlightIndices={activeHighlightIndices}
                            setActiveHighlightIndices={setActiveHighlightIndices}
                        />
                }
                
                <List>
                </List>
            </CustomDrawer>
        </div>
    )
}

export default TokenSidebar;