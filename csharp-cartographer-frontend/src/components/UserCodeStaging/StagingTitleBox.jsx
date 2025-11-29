import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Paper, Button } from '@mui/material';
import { TextField, InputLabel, Select, FormControl, MenuItem } from '@mui/material';
import colors from '../../utils/colors';

const TitleBoxContainer = styled(Box)(() => ({
    position: 'fixed',
    width: '99%',
    marginTop: '1.5rem',
    marginRight: '0.5rem',
    marginLeft: '0.5rem',
}));

const StyledPaper = styled(Paper)(() => ({
    alignItems: 'center',
    backgroundColor: colors.gray25,
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: "6px 12px",
    color: "#fff",
    minHeight: '62px'
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const GrowBox = styled(Box)(() => ({
    display: 'flex',
    flexGrow: 1
}));

const SaveArtifactBtn = styled(Button)(() => ({
    color: '#fff',
    background: 'linear-gradient(to right, #006650, #00cca0)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.2), 0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #004d3c, #00b38c)',
    },
}));

const StyledTextField = styled(TextField)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    '& .MuiInputBase-input': {
        color: '#fff',  // Input text color
        height: '40px',
        paddingTop: '0px',
        paddingBottom: '0px',
        fontSize: '1rem',
        fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    },
    '& .MuiOutlinedInput-root': {
        // '& fieldset': {
        //     borderColor: '#ff7733',  // Default border color
        // },
        '&:hover fieldset': {
            borderColor: '#000000',  // Border color on hover
            // backgroundColor: '#666666',
        },
        '&.Mui-focused fieldset': {
            borderColor: '#404040',  // Border color when focused
        },
        backgroundColor: '#595959',  // Input background color
    },
    '& .MuiInputLabel-root.Mui-focused': {
        color: '#00FFC7',  // Label color when focused
        fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    },
}));

const CustomSelectLabel = styled(InputLabel)(({ theme }) => ({
    color: '#00FFC7',
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    '&.Mui-focused': {
        color: '#00FFC7',
    },
}));

const CustomSelect = styled(Select)(({ theme }) => ({
    height: '40px',
    fontSize: '1rem',
    color: '#fff',  // Change selected item text color
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    backgroundColor: '#595959',  // Change dropdown background color
    '.MuiOutlinedInput-notchedOutline': {
        borderColor: '#404040',  // Change border color
    },
    '&:hover .MuiOutlinedInput-notchedOutline': {
        borderColor: '#00b38c',  // Change border color on hover
    },
    '&.Mui-focused .MuiOutlinedInput-notchedOutline': {
        borderColor: '#00FFC7',  // Change border color when focused
    },
}));

const StagingTitleBox = ({ artifactTemplateTitle, setArtifactTemplateTitle, artifactTemplateType, setArtifactTemplateType, artifactTemplateDescription, navTokens }) => {

    // Constants

    // State variables

    // Event handlers
    const handleArtifactNameChange = (event) => {
        setArtifactTemplateTitle(event.target.value);
    };

    const handleArtifactTypeChange = (event) => {
        setArtifactTemplateType(event.target.value);
    };

    const handleSaveArtifactClick = () => {
        // Send API request
        CreateArtifact();
    };

    // Use effects

    // API calls
    async function CreateArtifact() {
        try {
            // Create artifact
            const response = await axios.post("https://localhost:44300/Artifact/create-artifact", {
                title: artifactTemplateTitle,
                artifactType: artifactTemplateType,
                description: artifactTemplateDescription,
                navTokens: navTokens,
            });

            if (response.status === 200) {

            }

        } catch (error) {
            console.log('Create artifact API call failed.');
        }
    }
    
    return (
        <TitleBoxContainer>
            <StyledPaper>
                <FlexBox
                    alignItems="center"
                    gap={6}
                    sx={{
                        mt: 0.5
                    }}
                >
                    <FlexBox
                        sx={{
                            minWidth: 170,
                        }}
                    >
                        <StyledTextField
                            label="Title&nbsp;&nbsp;"
                            focused
                            type='text'
                            value={artifactTemplateTitle}
                            onChange={handleArtifactNameChange}
                        />
                    </FlexBox>
                    <FlexBox
                        sx={{
                            minWidth: 200
                        }}
                    >
                        <FormControl fullWidth size='small'>
                            <CustomSelectLabel id="demo-simple-select-label">Type</CustomSelectLabel>
                            <CustomSelect
                                id="demo-simple-select"
                                label="Type"
                                labelId="demo-simple-select-label"
                                value={artifactTemplateType}
                                onChange={handleArtifactTypeChange}
                            >
                                <MenuItem value={'Model Definition'}>Model Definition</MenuItem>
                                <MenuItem value={'API Controller'}>API Controller</MenuItem>
                                <MenuItem value={'Workflow Class'}>Workflow Class</MenuItem>
                                <MenuItem value={'Repository Class'}>Repository Class</MenuItem>
                            </CustomSelect>
                        </FormControl>
                    </FlexBox>

                    <GrowBox />

                    <SaveArtifactBtn
                        size='large'
                        endIcon={
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-floppy" viewBox="0 0 16 16">
                                <path d="M11 2H9v3h2z"/>
                                <path d="M1.5 0h11.586a1.5 1.5 0 0 1 1.06.44l1.415 1.414A1.5 1.5 0 0 1 16 2.914V14.5a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 14.5v-13A1.5 1.5 0 0 1 1.5 0M1 1.5v13a.5.5 0 0 0 .5.5H2v-4.5A1.5 1.5 0 0 1 3.5 9h9a1.5 1.5 0 0 1 1.5 1.5V15h.5a.5.5 0 0 0 .5-.5V2.914a.5.5 0 0 0-.146-.353l-1.415-1.415A.5.5 0 0 0 13.086 1H13v4.5A1.5 1.5 0 0 1 11.5 7h-7A1.5 1.5 0 0 1 3 5.5V1H1.5a.5.5 0 0 0-.5.5m3 4a.5.5 0 0 0 .5.5h7a.5.5 0 0 0 .5-.5V1H4zM3 15h10v-4.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5z"/>
                            </svg>
                        }
                        onClick={handleSaveArtifactClick}
                    >
                        Artifact
                    </SaveArtifactBtn>
                    
                </FlexBox>
            </StyledPaper>
        </TitleBoxContainer>
    );
}

export default StagingTitleBox;