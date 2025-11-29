import { styled } from '@mui/material/styles';
import { Box, Typography, Divider, List, Drawer, IconButton } from '@mui/material';
import { StagingLeftSidebarContent } from "../../components";
import colors from '../../utils/colors';
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';

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

const StagingLeftSidebar = ({leftSideBarOpen, setLeftSideBarOpen, artifactTemplateDescription, setArtifactTemplateDescription}) => {

    // Constants

    // State Variables
    
    // Event Handlers
    const handleDrawerToggle = () => {
        setLeftSideBarOpen(!leftSideBarOpen);
    };

    return (
        <div>
            {/* Drawer (Sidebar) */}
            <CustomDrawer
                variant="permanent"
                anchor="left"
                open={leftSideBarOpen}
                sx={{
                    width: leftSideBarOpen ? 430 : 50, // Full width when open, 30px when collapsed
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: leftSideBarOpen ? 380 : 50,
                        boxSizing: 'border-box',
                        transition: 'width 0.3s',
                        top: '150px', // Set to 150px to create a gap below the existing AppBar & TitleBox
                        height: 'calc(100% - 150px)', // Adjust height to fit screen and allow scrolling
                        backgroundColor: colors.gray25,
                        color: '#fff',
                        borderRadius: '0px 8px 0px 0px', // Rounded right edge
                        boxShadow: '0px 3px 5px -1px rgba(0, 0, 0, 0.6), 0px 5px 6px 0px rgba(0, 0, 0, 0.36), 1px 2px 11px 0px rgba(0, 0, 0, 0.32)',
                        overflow: 'auto'
                    },
                }}
            >
                {leftSideBarOpen
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
                                Artifact Description
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
                {!leftSideBarOpen
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
                            }}
                        >
                            Artifact Description
                        </Typography>
                    :
                        <StagingLeftSidebarContent />
                }
                
                <List>
                </List>
            </CustomDrawer>
        </div>
    );
}

export default StagingLeftSidebar;