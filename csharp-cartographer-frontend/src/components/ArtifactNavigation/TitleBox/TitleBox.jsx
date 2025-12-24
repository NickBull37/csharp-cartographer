import { styled } from '@mui/material/styles';
import { Box, Paper, Typography, Tooltip, Divider } from '@mui/material';
import ApiIcon from '@mui/icons-material/Api';
import colors from '../../../utils/colors';

const TitleBoxContainer = styled(Box)(() => ({
    position: 'fixed',
    width: '99%',
    marginTop: '1.5rem',
    marginRight: '0.75rem',
    marginLeft: '0.75rem',
}));

const StyledPaper = styled(Paper)(() => ({
    display: 'flex',
    backgroundColor: colors.gray25,
    boxShadow: '0px 2px 2px -1px rgba(0, 0, 0, 0.4), 0px 2px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: "0 8px 0 12px",
    color: "#fff",
    minHeight: '50px',
    alignItems: "center",
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const TitleText = styled(Typography)(() => ({
    fontSize: '1rem',
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    color: '#fff',
}));

const DataText = styled(Typography)(() => ({
    fontSize: '0.875rem',
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    color: '#e6e6e6',
}));

const TitleBox = ({ artifactTitle, leftSidebarOpen, numTokens, numTags, numAncestors, generationTime }) => {

    return (
        <TitleBoxContainer>
            <StyledPaper>
                <FlexBox
                    width="100%"
                >
                    <FlexBox
                        alignItems="center"
                        gap={1}
                    >
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" className="bi bi-globe-americas bootstrap-icon-fill-code" viewBox="0 0 16 16">
                            <path d="M8 0a8 8 0 1 0 0 16A8 8 0 0 0 8 0M2.04 4.326c.325 1.329 2.532 2.54 3.717 3.19.48.263.793.434.743.484q-.121.12-.242.234c-.416.396-.787.749-.758 1.266.035.634.618.824 1.214 1.017.577.188 1.168.38 1.286.983.082.417-.075.988-.22 1.52-.215.782-.406 1.48.22 1.48 1.5-.5 3.798-3.186 4-5 .138-1.243-2-2-3.5-2.5-.478-.16-.755.081-.99.284-.172.15-.322.279-.51.216-.445-.148-2.5-2-1.5-2.5.78-.39.952-.171 1.227.182.078.099.163.208.273.318.609.304.662-.132.723-.633.039-.322.081-.671.277-.867.434-.434 1.265-.791 2.028-1.12.712-.306 1.365-.587 1.579-.88A7 7 0 1 1 2.04 4.327Z"/>
                        </svg>
                        <TitleText>
                            {artifactTitle}
                        </TitleText>
                    </FlexBox>

                    <Divider
                        orientation='vertical'
                        flexItem
                        sx={{
                            py: '20px',
                            mx: '20px',
                            bgcolor: '#8c8c8c'
                        }}
                    />

                    <FlexBox
                        flexGrow={1}
                        justifyContent="space-between"
                        sx={{
                            px: 2
                        }}
                    >
                        <FlexBox
                            alignItems="center"
                            gap={1}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" className="bi bi-database bootstrap-icon-fill-code" viewBox="0 0 16 16">
                                <path d="M4.318 2.687C5.234 2.271 6.536 2 8 2s2.766.27 3.682.687C12.644 3.125 13 3.627 13 4c0 .374-.356.875-1.318 1.313C10.766 5.729 9.464 6 8 6s-2.766-.27-3.682-.687C3.356 4.875 3 4.373 3 4c0-.374.356-.875 1.318-1.313M13 5.698V7c0 .374-.356.875-1.318 1.313C10.766 8.729 9.464 9 8 9s-2.766-.27-3.682-.687C3.356 7.875 3 7.373 3 7V5.698c.271.202.58.378.904.525C4.978 6.711 6.427 7 8 7s3.022-.289 4.096-.777A5 5 0 0 0 13 5.698M14 4c0-1.007-.875-1.755-1.904-2.223C11.022 1.289 9.573 1 8 1s-3.022.289-4.096.777C2.875 2.245 2 2.993 2 4v9c0 1.007.875 1.755 1.904 2.223C4.978 15.71 6.427 16 8 16s3.022-.289 4.096-.777C13.125 14.755 14 14.007 14 13zm-1 4.698V10c0 .374-.356.875-1.318 1.313C10.766 11.729 9.464 12 8 12s-2.766-.27-3.682-.687C3.356 10.875 3 10.373 3 10V8.698c.271.202.58.378.904.525C4.978 9.71 6.427 10 8 10s3.022-.289 4.096-.777A5 5 0 0 0 13 8.698m0 3V13c0 .374-.356.875-1.318 1.313C10.766 14.729 9.464 15 8 15s-2.766-.27-3.682-.687C3.356 13.875 3 13.373 3 13v-1.302c.271.202.58.378.904.525C4.978 12.71 6.427 13 8 13s3.022-.289 4.096-.777c.324-.147.633-.323.904-.525"/>
                            </svg>
                            <DataText>
                                <span className='titlebox-data'>{(numTokens?.toLocaleString()) ?? 0}</span> tokens analyzed
                            </DataText>
                        </FlexBox>

                        <FlexBox
                            alignItems="center"
                            gap={1}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" className="bi bi-pin-map bootstrap-icon-fill-code" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M3.1 11.2a.5.5 0 0 1 .4-.2H6a.5.5 0 0 1 0 1H3.75L1.5 15h13l-2.25-3H10a.5.5 0 0 1 0-1h2.5a.5.5 0 0 1 .4.2l3 4a.5.5 0 0 1-.4.8H.5a.5.5 0 0 1-.4-.8z"/>
                                <path fill-rule="evenodd" d="M8 1a3 3 0 1 0 0 6 3 3 0 0 0 0-6M4 4a4 4 0 1 1 4.5 3.969V13.5a.5.5 0 0 1-1 0V7.97A4 4 0 0 1 4 3.999z"/>
                            </svg>
                            <DataText>
                                <span className='titlebox-data'>{(numTags?.toLocaleString()) ?? 0}</span> pins dropped
                            </DataText>
                        </FlexBox>

                        <FlexBox
                            alignItems="center"
                            gap={1}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" class="bi bi-map bootstrap-icon-fill-code" viewBox="0 0 16 16">
                                <path fill-rule="evenodd" d="M15.817.113A.5.5 0 0 1 16 .5v14a.5.5 0 0 1-.402.49l-5 1a.5.5 0 0 1-.196 0L5.5 15.01l-4.902.98A.5.5 0 0 1 0 15.5v-14a.5.5 0 0 1 .402-.49l5-1a.5.5 0 0 1 .196 0L10.5.99l4.902-.98a.5.5 0 0 1 .415.103M10 1.91l-4-.8v12.98l4 .8zm1 12.98 4-.8V1.11l-4 .8zm-6-.8V1.11l-4 .8v12.98z"/>
                            </svg>
                            <DataText>
                                <span className='titlebox-data'>{(numAncestors?.toLocaleString()) ?? 0}</span> ancestors mapped
                            </DataText>
                        </FlexBox>

                        <FlexBox
                            alignItems="center"
                            gap={1}
                        >
                            <svg xmlns="http://www.w3.org/2000/svg" width="17" height="17" class="bi bi-stopwatch bootstrap-icon-fill-code" viewBox="0 0 16 16">
                                <path d="M8.5 5.6a.5.5 0 1 0-1 0v2.9h-3a.5.5 0 0 0 0 1H8a.5.5 0 0 0 .5-.5z"/>
                                <path d="M6.5 1A.5.5 0 0 1 7 .5h2a.5.5 0 0 1 0 1v.57c1.36.196 2.594.78 3.584 1.64l.012-.013.354-.354-.354-.353a.5.5 0 0 1 .707-.708l1.414 1.415a.5.5 0 1 1-.707.707l-.353-.354-.354.354-.013.012A7 7 0 1 1 7 2.071V1.5a.5.5 0 0 1-.5-.5M8 3a6 6 0 1 0 .001 12A6 6 0 0 0 8 3"/>
                            </svg>
                            <DataText>
                                <span className='titlebox-data'>{generationTime}</span> to generate
                            </DataText>
                        </FlexBox>
                    </FlexBox>

                    <FlexBox
                        alignItems="center"
                        gap={0.5}
                    >
                        <Divider
                            orientation='vertical'
                            flexItem
                            sx={{
                                mx: '14px',
                                bgcolor: '#8c8c8c'
                            }}
                        />
                        <Tooltip
                            title={leftSidebarOpen
                                ? "Highlight Mode: used to highlight tokens to send to AI Analysis"
                                : "Select Mode: used to select individual tokens to analyze"}
                        >
                            <ApiIcon
                                fontSize='large'
                                sx={{
                                    color: leftSidebarOpen ? colors.pinkDark : colors.orange
                                }}
                            />
                        </Tooltip>
                        <Tooltip title="Highlight mode is activated whenever the left sidebar is open and deactivated whenever the left sidebar is closed">
                            <ApiIcon
                                sx={{
                                    color: leftSidebarOpen ? colors.orange : colors.pinkDark,
                                    marginTop: '18px',
                                    p: 0,
                                    height: '16px'
                                }}
                            />
                        </Tooltip>
                    </FlexBox>

                </FlexBox>
            </StyledPaper>
        </TitleBoxContainer>
    );
}

export default TitleBox;