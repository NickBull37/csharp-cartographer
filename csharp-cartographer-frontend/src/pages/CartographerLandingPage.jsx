import React from 'react';
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography } from '@mui/material';
import { AppBar, Toolbar, IconButton, Avatar, Button } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import SettingsIcon from '@mui/icons-material/Settings';
import MapOutlinedIcon from '@mui/icons-material/MapOutlined';
import { compass, compass3, compass4, cartographer } from '../utils/constants';
import colors from '../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const DemoBtn = styled(Button)(() => ({
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

const CartographerLandingPage = () => {

    return (
        <Stack>
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                mt={14}
            >
                <span className='cartographer'>C</span>
                <span className='hashtag'>#</span>
                <span className='cartographer'>RTOGRAPHER</span>
            </Box>
            <Box
                display="flex"
                justifyContent="center"
                alignItems="center"
                mt={16}
                sx={{
                    p: 8,
                    bgcolor: '#262626'
                }}
            >
                <span className='dot'>.</span>
                <span className='net'>NET</span>
                <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <span className='cartographer1'>C</span>
                <span className='hashtag-2'>#</span>
                <span className='cartographer2'>RTOGRAPHER</span>
            </Box>
        </Stack>
    );
}

export default CartographerLandingPage;