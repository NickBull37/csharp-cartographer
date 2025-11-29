import React from 'react';
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Divider } from '@mui/material';
import { AppBar, Toolbar, IconButton, Avatar, Button } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import SettingsIcon from '@mui/icons-material/Settings';
import MapOutlinedIcon from '@mui/icons-material/MapOutlined';
import { compass, compass3, compass4, cartographer, orgCartographer, newCartographer } from '../../../utils/constants';
import colors from '../../../utils/colors';

const StyledAppBar = styled(AppBar)(() => ({
    backgroundColor: colors.gray15,
    minHeight: '55px',
    justifyContent: 'center',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const NavLinkBtn = styled(Button)(() => ({
    height: '25px',
    width: '100px',
    color: '#fff',
    background: 'linear-gradient(to right, #006650, #00b38c)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #004d3c, #009978)',
    },
}));

const Navbar = () => {

    return (
        <Box sx={{ flexGrow: 1 }}>
            <StyledAppBar position="fixed">
                <Toolbar
                    variant="dense"
                >
                    <Link to="/">
                        <Box
                            display="flex"
                            alignItems="center"
                            sx={{
                                
                            }}
                        >
                            <img className='nav-logo' src={compass3} height={33} />
                            <img className='cartographer-logo' src={newCartographer} height={33} />
                        </Box>
                    </Link>
                    <FlexBox
                        flexGrow={1}
                        justifyContent="flex-end"
                        alignItems="center"
                    >
                        <FlexBox
                            gap={4}
                            sx={{
                                mr: 2
                            }}
                        >
                            <NavLinkBtn
                                size='small'
                                startIcon={<MapOutlinedIcon fontSize='small' sx={{ color: '#fff' }} />}
                                href='/demo-options'
                            >
                                Demo
                            </NavLinkBtn>
                            <NavLinkBtn
                                size='small'
                                startIcon={<AddIcon fontSize='small' sx={{ color: '#fff' }} />}
                                href='/upload'
                            >
                                Upload
                            </NavLinkBtn>
                        </FlexBox>
                        
                        <Box
                            sx={{
                                height: '35px',
                                borderRight: '2px solid #4d4d4d',
                                mx: 2
                            }}
                        >
                        </Box>

                        <FlexBox
                            alignItems="center"
                            gap={3}
                        >
                            <IconButton
                                size="small"
                            >
                                <SettingsIcon sx={{ color: '#fff' }} />
                            </IconButton>
                            <Avatar
                                alt="Remy Sharp"
                                src="/broken-image.jpg"
                                sx={{ width: 24, height: 24, bgcolor: '#cc33ff' }}
                            >
                                N
                            </Avatar>
                        </FlexBox>
                    </FlexBox>
                </Toolbar>
            </StyledAppBar>
            <Toolbar variant="dense" />
        </Box>
    );
}

export default Navbar;