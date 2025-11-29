import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Stack, Button, IconButton, Typography, TextField } from '@mui/material';
import CloudUploadIcon from '@mui/icons-material/CloudUpload';
import FilePresentIcon from '@mui/icons-material/FilePresent';
import CloseIcon from '@mui/icons-material/Close';

const VisuallyHiddenInput = styled('input')({
    clip: 'rect(0 0 0 0)',
    clipPath: 'inset(50%)',
    height: 1,
    overflow: 'hidden',
    position: 'absolute',
    bottom: 0,
    left: 0,
    whiteSpace: 'nowrap',
    width: 1,
});

const UploadButton = styled(Button)(() => ({
    color: '#00F9A5',
    border: '1px solid #00F9A5',
    '&:hover': {
        color: '#00cc88',
        border: '1px solid #00cc88',
    },
}));

const GenerateButton = styled(Button)(() => ({
    backgroundColor: '#28a470',
    '&:hover': {
        backgroundColor: '#1e7b54',
    },
}));

const PageContainer = styled(Box)(() => ({
    minHeight: '100vh',
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    height: '70vh',
    paddingTop: '200px',
    justifyContent: 'space-around',
    alignItems: 'center'
}));

const UserCodeUpload = ({setArtifact}) => {

    // Constants
    const navigate = useNavigate();

    // State Variables
    const [generateFileBtnEnabled, setGenerateFileBtnEnabled] = useState(false);

    const [fileName, setFileName] = useState('');
    const [fileContent, setFileContent] = useState('');
    const [filePresent, setFilePresent] = useState(false);

    // Event Handlers
    const handleUpload = (event) => {
        const file = event.target.files[0];
        if (file) {
            setFileName(file.name);
            setFilePresent(true);

            const reader = new FileReader();
            reader.onload = (e) => {
                setFileContent(e.target.result); // Save the file content
            };
            reader.readAsText(file); // Read the file content as text
        }
    };

    const handleClearFile = () => {
        setFileName('');
        setFileContent('');
        setFilePresent(false);
        setGenerateFileBtnEnabled(false);
    };

    const handleGenerateFromFile = () => {
        // Send API request
        GenerateArtifact();
    };

    // Use Effects
    useEffect(() => {
        setGenerateFileBtnEnabled(!!fileName); // Enable button if file is provided
    }, [fileName]);

    // API Calls
    async function GenerateArtifact() {
        try {
            // Create artifact
            const response = await axios.post("https://localhost:44300/Artifact/generate-artifact", {
                fileName: fileName,
                fileContent: fileContent
            });

            if (response.status === 200) {
                setArtifact(response.data);
                // navigate('/staging');
                navigate('/cartograph');
            }
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <PageContainer>
            <FlexBox>
                <Stack
                    display={'flex'}
                    width={'40%'}
                    alignItems={'center'}
                    gap={6}
                >
                    <Typography
                        className='code'
                        sx={{
                            fontSize: '1.5rem',
                        }}
                    >
                        [Upload a C# source file]
                    </Typography>
                    <Typography
                        className='code'
                        sx={{
                            fontSize: '0.875rem',
                        }}
                    >
                        Uploaded source code files need to be valid C# code in order for the Roslyn library to properly parse and analyze it.
                    </Typography>
                    <Stack gap={2}>
                        <UploadButton
                            variant="outlined"
                            component="label"
                            tabIndex={-1}
                            startIcon={<CloudUploadIcon />}
                            sx={{
                                mt: 2
                            }}
                        >
                            Upload file
                            <VisuallyHiddenInput
                                type="file"
                                onChange={handleUpload}
                            />
                        </UploadButton>
                        <Box
                            display={filePresent ? 'flex' : 'none'}
                            alignItems={'center'}
                            mt={1.5}
                        >
                            <FilePresentIcon
                                fontSize='small'
                                color='#d9d9d9'
                            />
                            <Typography
                                sx={{
                                    color: '#d9d9d9'
                                }}
                            >
                                &nbsp;{fileName}&nbsp;
                            </Typography>
                            <IconButton
                                size="small"
                                onClick={handleClearFile}
                                sx={{
                                    color: '#d9d9d9',
                                    '&:hover': {
                                        backgroundColor: '#555555',
                                    },
                                }}
                            >
                                <CloseIcon fontSize="small" sx={{ color: '#d9d9d9' }} />
                            </IconButton>
                        </Box>
                        <GenerateButton
                            variant="contained"
                            disabled={!generateFileBtnEnabled}
                            onClick={handleGenerateFromFile}
                            sx={{
                                mt: 4
                            }}
                        >
                            Generate NavDoc
                        </GenerateButton>
                    </Stack>
                </Stack>
                
            </FlexBox>
        </PageContainer>
    );
}

export default UserCodeUpload;