import { styled } from '@mui/material/styles';
import { Box, Typography, Divider } from '@mui/material';
import { Drawer, IconButton, List } from '@mui/material';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { StructureSidebarContent } from "../../index";
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

const StructureSidebar = ({ leftSidebarOpen, setLeftSidebarOpen }) => {

    const handleDrawerToggle = () => {
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
                        top: '150px', // set to 150px to create a gap below the existing AppBar & TitleBox
                        height: 'calc(100% - 150px)', // adjust height to fit screen and allow scrolling
                        backgroundColor: colors.gray25,
                        color: '#fff',
                        borderRadius: '0px 4px 0px 0px', // rounded right edge
                        boxShadow: '0px 3px 5px -1px rgba(0, 0, 0, 0.6), 0px 5px 6px 0px rgba(0, 0, 0, 0.36), 1px 2px 11px 0px rgba(0, 0, 0, 0.32)',
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
                                Structure Analysis
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
                            Structural Analysis
                        </Typography>
                    :
                        <StructureSidebarContent />
                }
                
                <List>
                </List>
            </CustomDrawer>
        </div>
    )
}

export default StructureSidebar;