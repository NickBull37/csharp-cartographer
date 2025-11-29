import { styled } from '@mui/material/styles';
import { Stack, Typography, TextField } from '@mui/material';

const TitleText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '18px',
    color: '#00FFC7',
    marginBottom: '4px'
}));

const StagingLeftSidebarContent = ({artifactTemplateDescription, setArtifactTemplateDescription}) => {
    
    const handleArtifactDescriptionChange = (event) => {
        setArtifactTemplateDescription(event.target.value);
    };

    return (
        <Stack
            sx={{
                padding: '10px',
            }}
        >
            <TitleText>
                Description
            </TitleText>
            <TextField
                fullWidth
                id="artifact-desc"
                label="Add artifact description"
                multiline
                rows={18}
                defaultValue=""
                variant="filled"
                onChange={handleArtifactDescriptionChange}
                sx={{
                    borderRadius: '4px',
                    '& .MuiFilledInput-root': {
                            color: '#fff',
                            backgroundColor: '#666666',
                        },
                    '& .MuiInputLabel-root': {
                        color: '#cccccc',
                    },
                    '& .MuiInputLabel-root.Mui-focused': {
                        color: '#cccccc',
                    },
                    '& .MuiFilledInput-root:hover': {
                        backgroundColor: '#737373',
                    },
                    '& .MuiFilledInput-root.Mui-focused': {
                        backgroundColor: '#666666',
                    }
                }}
            />
        </Stack>
    );
}

export default StagingLeftSidebarContent;