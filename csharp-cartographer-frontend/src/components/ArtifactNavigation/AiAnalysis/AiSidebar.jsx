import { styled } from '@mui/material/styles';
import { Box, Typography, Divider } from '@mui/material';
import { Drawer, IconButton, List } from '@mui/material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { AiSidebarContent } from "../../index";
import colors from '../../../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const CustomDrawer = styled(Drawer)(() => ({
    '& .MuiDrawer-paper': {
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

const AiSidebar = ({ navTokens, leftSidebarOpen, setLeftSidebarOpen, selectedTokens, setSelectedTokens }) => {

    const handleDrawerToggle = () => {
        setSelectedTokens([]);
        setLeftSidebarOpen(!leftSidebarOpen);
    };

    return (
        <div>
            <CustomDrawer
                variant="permanent"
                anchor="left"
                open={leftSidebarOpen}
                sx={{
                    width: leftSidebarOpen ? 430 : 50, // full width when open, 30px when collapsed
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: leftSidebarOpen ? 380 : 50,
                        boxSizing: 'border-box',
                        transition: 'width 0.3s',
                        top: '140px',
                        height: 'calc(100% - 140px)',
                        backgroundColor: colors.gray25,
                        color: '#fff',
                        borderRadius: '0px 4px 0px 0px',
                        boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
                        overflow: 'auto'
                    },
                }}
            >

                {leftSidebarOpen
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
                                    ml: 2.5
                                }}
                            >
                                AI Analysis
                            </Typography>
                            <IconButton
                                onClick={handleDrawerToggle}
                                sx={{
                                    color: '#cccccc'
                                }}
                            >
                                <KeyboardDoubleArrowLeftIcon />
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
                            <KeyboardDoubleArrowRightIcon />
                        </IconButton>
                }

                <Divider sx={{ bgcolor: '#808080' }} />

                {!leftSidebarOpen
                    ?
                        <Typography
                            className='code'
                            sx={{
                                color: '#cccccc',
                                position: 'absolute',
                                top: '47%',
                                left: '50%',
                                transform: 'translate(-50%, -50%) rotate(90deg)', // Center and rotate text
                                whiteSpace: 'nowrap',
                                fontSize: '1.25rem',
                                textShadow: '1px 1px 2px #000, 0 0 16px #333333, 0 0 5px #1a1a1a',
                            }}
                        >
                            AI Analysis
                        </Typography>
                    :
                        <AiSidebarContent
                            navTokens={navTokens}
                            selectedTokens={selectedTokens}
                            setSelectedTokens={setSelectedTokens}
                        />
                }
                
                <List>
                </List>
            </CustomDrawer>
        </div>
    )
}

export default AiSidebar;