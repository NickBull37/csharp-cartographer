import { useState, useEffect } from 'react';
import axios from "axios";
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Button, Stack, Typography } from '@mui/material';
import MapOutlinedIcon from '@mui/icons-material/MapOutlined';
import colors from '../utils/colors';

const PageContainer = styled(Box)(() => ({
    minHeight: '100%',
    paddingRight: 2,
    paddingLeft: 2,
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    paddingTop: '140px',
    alignItems: 'center'
}));

const BorderBoxLeft = styled(Box)(() => ({
    display: 'flex',
    height: '33vh',
    borderRight: '1px solid #00e6cf',
}));

const BorderBoxRight = styled(Box)(() => ({
    display: 'flex',
    height: '33vh',
    borderLeft: '1px solid #00e6cf',
}));

const BtnText = styled(Typography)(() => ({
    fontSize: '1.063rem',
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    transition: 'transform 0.3s ease',
    '&:hover': {
          transform: 'translateY(-4px)',
          color: colors.code,
          cursor: 'pointer'
    },
}));

const InfoText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '0.938rem',
}));

const ViewDemoBtn = styled(Button)(() => ({
    height: '45px',
    width: '160px',
    color: '#fff',
    background: 'linear-gradient(to right, #006650, #00b38c)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #004d3c, #009978)',
    },
}));

const DemoOptionSelection = () => {

    // State Variables
    const [selectedDemo, setSelectedDemo] = useState(null);
    const [demoFiles, setDemoFiles] = useState([]);

    // Event Handlers
    const handleDemoFileClick = (fileName) => {
        setSelectedDemo(demoFiles.find(file => file.name === fileName));
    };

    // Use Effects
    useEffect(() => {
        GetDemoFiles();
    }, []);

    // Api Calls
    async function GetDemoFiles() {
        try {
            const response = await axios.get("https://localhost:44300/DemoOptions/get-demo-options");

            if (response.status === 200) {
                setDemoFiles(response.data);
                setSelectedDemo(response.data[0]);
            }
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <PageContainer>
            <Stack gap={8}>
                <FlexBox>

                    <Stack
                        width={'30%'}
                        alignItems={'center'}
                    >
                        <Stack
                            // gap={4.5}
                            gap={2}
                        >
                            {demoFiles.map((file) => (
                                <BtnText
                                    onClick={() => handleDemoFileClick(file.name)}
                                    sx={{
                                        color: selectedDemo?.name === file.name ? colors.code : '#fff'
                                    }}
                                >
                                    [{file.name}]
                                </BtnText>
                            ))}
                        </Stack>
                    </Stack>

                    <Box>
                        <BorderBoxLeft />
                        <BorderBoxRight />
                    </Box>

                    <Stack
                        width={'40%'}
                        gap={3}
                        sx={{
                            px: 8
                        }}
                    >
                        <InfoText
                            className='color-code'
                            textAlign={"center"}
                            sx={{
                                fontSize: '1.65rem'
                            }}
                        >
                            {selectedDemo?.type}
                        </InfoText>
                        <Stack
                            display={'flex'}
                            gap={4}
                            sx={{
                                bgcolor: colors.gray25,
                                padding: 2,
                                borderRadius: '5px'
                            }}
                        >
                            {selectedDemo?.insights.map((insight) => (
                                <InfoText>
                                    {insight}
                                </InfoText>
                            ))}
                        </Stack>
                    </Stack>

                    <Box>
                        <BorderBoxLeft />
                        <BorderBoxRight />
                    </Box>

                    <Box
                        display="flex"
                        justifyContent="center"
                        width='30%'
                    >
                        <Stack
                            display="flex"
                            alignItems="center"
                            gap={4}
                            sx={{
                                px: 6,
                            }}
                        >
                            <Typography
                                className='code'
                                sx={{
                                    fontSize: '0.875rem'
                                }}
                            >
                                All demo artifacts were created using C# source code files from this application.
                            </Typography>
                            <Typography
                                className='code'
                                sx={{
                                    fontSize: '0.875rem'
                                }}
                            >
                                Try generating a brand new artifact by uploading your own C# code <a className='color-code' href="/upload">here</a>. 
                            </Typography>
                            <Link
                                to={`/cartographer-demo?file=${encodeURIComponent(selectedDemo?.name)}`}
                                style={{ textDecoration: 'none' }}
                            >
                                <ViewDemoBtn
                                    size='large'
                                    startIcon={<MapOutlinedIcon sx={{ color: '#fff' }} />}
                                    sx={{
                                        mt: 4
                                    }}
                                >
                                    View Demo
                                </ViewDemoBtn>
                            </Link>
                        </Stack>
                    </Box>
                </FlexBox>
            </Stack>
        </PageContainer>
    );
}

export default DemoOptionSelection;