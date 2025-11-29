import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography, TextField } from '@mui/material';
import { ToggleButton, Divider, Button } from '@mui/material';
import CheckIcon from '@mui/icons-material/Check';
import SendIcon from '@mui/icons-material/Send';
import colors from '../../../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
    paddingRight: '12px',
    paddingLeft: '12px',
    paddingTop: '10px',
    height: '100%',
}));

const SelectedCountText = styled(Typography)(() => ({
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    fontSize: '1.125rem',
    color: colors.code,
}));

const ResponseText = styled(Typography)(() => ({
    fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
    fontSize: '13px',
    letterSpacing: '0.04em',
    color: colors.gray95,
    border: '1px solid #808080',
    borderRadius: '4px',
    padding: '8px',
}));

const AiSubmitBtn = styled(Button)(() => ({
    height: '25px',
    color: '#fff',
    background: 'linear-gradient(to right, #006650, #00b38c)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: '3px 0 2px 0',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #004d3c, #009978)',
    },
}));

const AiSidebarContent = ({navTokens, selectedTokens, setSelectedTokens}) => {

    // State Variables
    const [sendAllSelected, setSendAllSelected] = useState(false);
    const [aiResponse, setAiResponse] = useState('Select the tokens you want more insight on and click the Ask AI button to receive an in-depth breakdown of the selected tokens.');

    // Use Effects
    useEffect(() => {
        if (sendAllSelected) {
            setSelectedTokens(navTokens);
        }
        else {
            setSelectedTokens([]);
        }
    }, [sendAllSelected]);

    // API Calls
    async function GetAiAnalysis() {

        // implement this

    }

    return (
        <ContentContainer>
            <Stack
                gap={2.5}
            >
                <FlexBox
                    justifyContent="space-evenly"
                >
                    <FlexBox
                        gap={2}
                        alignItems="center"
                    >
                        <Typography>
                            Send all tokens
                        </Typography>
                        <ToggleButton
                            value="check"
                            selected={sendAllSelected}
                            onChange={() => setSendAllSelected((prevSendAllSelected) => !prevSendAllSelected)}
                            sx={{
                                height: '22px',
                                width: '22px',
                                color: '#bfbfbf',
                                borderColor: '#a6a6a6',
                                '&.Mui-selected': {
                                    backgroundColor: '#00b38c',
                                    color: '#ffffff',
                                    borderColor: '#808080',
                                    '&:hover': {
                                        backgroundColor: '#00b3a1',
                                    },
                                },
                            }}
                        >
                            {sendAllSelected
                                ? 
                                    <CheckIcon />
                                : 
                                    <></>
                            }
                        </ToggleButton>
                    </FlexBox>
                    <Divider orientation='vertical' flexItem sx={{ bgcolor: '#8c8c8c' }} />
                    <FlexBox
                        gap={2}
                        alignItems="center"
                    >
                        <Typography>
                            Selected: 
                        </Typography>
                        <SelectedCountText>
                            {selectedTokens.length - 1 < 0 ? 0 : selectedTokens.length - 1} 
                        </SelectedCountText>
                    </FlexBox>
                </FlexBox>
                <AiSubmitBtn
                    size='small'
                    endIcon={
                        <SendIcon
                            sx={{
                                color: '#fff',
                                height: '14px',
                                width: '14px',
                                mt: '-2px'
                            }}
                        />
                    }
                >
                    Send tokens
                </AiSubmitBtn>
                <ResponseText
                    flexGrow={1}
                >
                    {aiResponse}
                </ResponseText>
            </Stack>
        </ContentContainer>
    );
}

export default AiSidebarContent;