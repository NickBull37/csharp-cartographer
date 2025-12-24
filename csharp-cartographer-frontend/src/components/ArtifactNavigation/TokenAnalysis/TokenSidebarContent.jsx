import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography, IconButton, Tooltip, Divider } from '@mui/material';
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';
import ArrowBackIosNewIcon from '@mui/icons-material/ArrowBackIosNew';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ExploreIcon from '@mui/icons-material/Explore';
import ExploreOutlinedIcon from '@mui/icons-material/ExploreOutlined';
import KeyIcon from '@mui/icons-material/Key';
import CodeIcon from '@mui/icons-material/Code';
import LinkIcon from '@mui/icons-material/Link';

import colors from '../../../utils/colors';

const FlexBoxCenterY = styled(Box)(() => ({
    display: 'flex',
    alignItems: 'center',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
}));

const OrangeBox = styled(Box)(() => ({
    padding: '12px 12px',
    //border: '1px solid rgba(255, 136, 77, 0.7)',
    borderRadius: '4px',
    backgroundColor: 'rgba(204, 82, 0, 0.175)',
    marginBottom: '4px',
}));

const TealBox = styled(Box)(() => ({
    padding: '4px 8px',
    border: '1px solid rgba(0, 204, 184, 0.7)',
    borderRadius: '3px',
    backgroundColor: 'rgba(0, 204, 184, 0.05)'
}));

const GrayBox = styled(Box)(() => ({
    padding: '12px 12px',
    borderRadius: '4px',
    backgroundColor: 'rgba(51, 51, 51, 0.575)',
    marginBottom: '4px',
}));

const TagDefinitionBox = styled(Box)(() => ({
    padding: '12px 12px',
    borderRadius: '4px',
    backgroundColor: colors.gray30,
    border: '1px solid #333333',
}));

const TokenText = styled(Typography)(() => ({
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    fontSize: '1rem',
}));

const BoxText = styled(Typography)(() => ({
    fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
    fontSize: '13px',
    letterSpacing: '0.04em',
    color: colors.gray95
}));

const PrevTokenButton = styled(IconButton)(() => ({
    width: '25px',
    height: '50px',
    borderRadius: '1px',
    borderRight: '1px solid #666666',
    '&:hover': {
        backgroundColor: '#333333',
    },
}));

const NextTokenButton = styled(IconButton)(() => ({
    width: '25px',
    height: '50px',
    borderRadius: '1px',
    borderLeft: '1px solid #666666',
    '&:hover': {
        backgroundColor: '#333333',
    },
}));

const TokenSidebarContent = ({ navTokens, activeToken, setActiveToken, activeHighlightIndices, setActiveHighlightIndices }) => {

    // Find the index of the activeToken in navTokens
    const activeTokenIndex = navTokens.findIndex(token => token === activeToken);

    // State Variables
    const [tokenText, setTokenText] = useState('');
    const [tokenLabel, setTokenLabel] = useState('');
    const [roslynClassification, setRoslynClassification] = useState('');
    const [updatedClassification, setUpdatedClassification] = useState('');
    const [tokenDefinition, setTokenDefinition] = useState('');
    const [comprehensiveElement, setComprehensiveElement] = useState('');
    const [highlightColor, setHighlightColor] = useState('');
    const [tokenTags, setTokenTags] = useState([]);
    const [charts, setCharts] = useState([]);
    const [activeTag, setActiveTag] = useState(null);
    const [activeChart, setActiveChart] = useState(null);
    const [tokenChartExpanded, setTokenChartExpanded] = useState(true);

    // Event Handlers
    const handleNextTokenClick = () => {

        // Clear insight highlighting
        //setInsightHighlightIndexes([]);

        let nextIndex = activeTokenIndex + 1;

        // loop to skip any tokens that are just spaces
        while (nextIndex < navTokens.length
            && (navTokens[nextIndex].text.trim() === ""
            || navTokens[nextIndex].text === "<newline>")) {
            nextIndex++;
        }

        // check if the next valid token exists and set it as the active token
        if (nextIndex < navTokens.length) {
            setActiveToken(navTokens[nextIndex]);
        }
    };

    const handlePrevTokenClick = () => {

        // Clear insight highlighting
        //setInsightHighlightIndexes([]);

        let prevIndex = activeTokenIndex - 1;

        // loop to skip any tokens that are just spaces
        while (prevIndex >= 0
            && (navTokens[prevIndex].text.trim() === ""
            || navTokens[prevIndex].text === "<newline>")) {
            prevIndex--;
        }

        // check if the previous valid token exists and set it as the active token
        if (prevIndex >= 0) {
            setActiveToken(navTokens[prevIndex]);
        }
    };

    const handleExpandTokenChartClick = () => {
        setTokenChartExpanded(prev => !prev);
    };

    const handleTagClick = (tag) => {
        if (activeTag === tag) {
            setActiveTag(null);
        }
        else {
            setActiveTag(tag); // update the active tag on click
        }
    };

    const toggleChartHighlighting = (tag) => {
        if (tag.highlightIndices.length === 0 || activeHighlightIndices === tag.highlightIndices) {
            setActiveHighlightIndices([]);
        }
        else {
            console.log(tag.highlightIndices);
            setActiveHighlightIndices(tag.highlightIndices);
        }
    };

    const handleExpandChartClick = (chart) => {
        if (activeChart === chart) {
            setActiveChart(null);
        }
        else {
            setActiveChart(chart); // update the active chart on click
        }
    };

    // Use Effects
    useEffect(() => {
        if (activeToken) { // check activeToken is not null or undefined
            setTokenText(activeToken.text || '');
            setTokenLabel(activeToken.label || '');
            setUpdatedClassification(activeToken.updatedClassification || '');
            setRoslynClassification(activeToken.roslynClassification || '');
            setTokenDefinition(activeToken.definition || '');
            setComprehensiveElement(activeToken.comprehensiveElement || '');
            setHighlightColor(activeToken.highlightColor || '');
            setTokenTags(activeToken.tags || []);
            setCharts(activeToken.charts || []);
        } else {
            // if activeToken is null or undefined, reset states
            setTokenText('');
            setTokenLabel('');
            setUpdatedClassification('');
            setTokenDefinition('');
            setComprehensiveElement('');
            setHighlightColor('');
            setTokenTags([]);
            setCharts([]);
        }
    }, [activeToken]);

    return (
        <ContentContainer>
            <Stack
                sx={{
                    width: '100%',
                }}
            >
                <FlexBoxCenterY
                    justifyContent="space-between"
                    sx={{
                        px: 0.5
                    }}
                >
                    <Tooltip title="Prev Token">
                        <PrevTokenButton
                            onClick={handlePrevTokenClick}
                        >
                            <ArrowBackIosNewIcon
                                sx={{
                                    color: '#d9d9d9',
                                    fontSize: '14px',
                                    mr: 0.5
                                }}
                            />
                        </PrevTokenButton>
                    </Tooltip>
                    <TokenText
                        className={
                            highlightColor === 'color-white'
                                    ? 'active-white'
                                : highlightColor === 'color-gray'
                                    ? 'active-gray'
                                : highlightColor === 'color-blue'
                                    ? 'active-blue'
                                : highlightColor === 'color-light-blue'
                                    ? 'active-light-blue'
                                : highlightColor === 'color-dark-blue'
                                    ? 'active-dark-blue'
                                : highlightColor === 'color-green'
                                    ? 'active-green'
                                : highlightColor === 'color-light-green'
                                    ? 'active-light-green'
                                : highlightColor === 'color-dark-green'
                                    ? 'active-dark-green'
                                : highlightColor === 'color-purple'
                                    ? 'active-purple'
                                : highlightColor === 'color-orange'
                                    ? 'active-orange'
                                : highlightColor === 'color-yellow'
                                    ? 'active-yellow'
                                : 'default-text-class'
                        }
                    >
                        {tokenText}
                        {/* {tokenText} - {activeTokenIndex} */}
                    </TokenText>
                    <Tooltip title="Next Token">
                        <NextTokenButton
                            onClick={handleNextTokenClick}
                            >
                            <ArrowForwardIosIcon
                                sx={{
                                    color: '#d9d9d9',
                                    fontSize: '14px',
                                    ml: 0.5
                                }}
                            />
                        </NextTokenButton>
                    </Tooltip>
                </FlexBoxCenterY>

                <Stack gap={1}>

                    <Stack>

                        <Divider sx={{ bgcolor: '#808080' }} />
                        <Typography
                            className='code'
                            sx={{
                                px: 1,
                                color: colors.gray60,
                                fontSize: '14px',
                                textAlign: 'center',
                                py: '2px'
                            }}
                        >
                            {roslynClassification}
                        </Typography>

                        <Divider sx={{ bgcolor: '#808080' }} />
                        <Typography
                            className='code'
                            sx={{
                                px: 1,
                                color: colors.gray60,
                                fontSize: '14px',
                                textAlign: 'center',
                                py: '2px'
                            }}
                        >
                            {updatedClassification}
                        </Typography>

                        <Divider sx={{ bgcolor: '#808080' }} />

                        <Box
                            display="flex"
                            justifyContent="space-between"
                            alignItems="center"
                            sx={{
                                pt: 1,
                                px: 1.15
                            }}
                        >
                            <Box
                                display="flex"
                                alignItems="center"
                            >
                                {/* <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" className="bi bi-book bootstrap-icon-fill-gray" viewBox="0 0 16 16">
                                    <path d="M1 2.828c.885-.37 2.154-.769 3.388-.893 1.33-.134 2.458.063 3.112.752v9.746c-.935-.53-2.12-.603-3.213-.493-1.18.12-2.37.461-3.287.811zm7.5-.141c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783"/>
                                </svg> */}
                                <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" className="bi bi-pin-map bootstrap-icon-fill-gray" viewBox="0 0 16 16">
                                    <path fill-rule="evenodd" d="M3.1 11.2a.5.5 0 0 1 .4-.2H6a.5.5 0 0 1 0 1H3.75L1.5 15h13l-2.25-3H10a.5.5 0 0 1 0-1h2.5a.5.5 0 0 1 .4.2l3 4a.5.5 0 0 1-.4.8H.5a.5.5 0 0 1-.4-.8z"/>
                                    <path fill-rule="evenodd" d="M8 1a3 3 0 1 0 0 6 3 3 0 0 0 0-6M4 4a4 4 0 1 1 4.5 3.969V13.5a.5.5 0 0 1-1 0V7.97A4 4 0 0 1 4 3.999z"/>
                                </svg>
                                <Typography
                                    className='code'
                                    sx={{
                                        px: 1,
                                        color: colors.gray60,
                                        fontSize: '14px'
                                    }}
                                >
                                    Token Pins
                                </Typography>
                            </Box>
                            <IconButton
                                onClick={() => handleExpandTokenChartClick()}
                                sx={{
                                    p: 0,
                                    m: 0
                                }}
                            >
                                <ExpandMoreIcon
                                    fontSize='small'
                                    sx={{
                                        color: colors.gray70
                                    }}
                                />
                            </IconButton>
                        </Box>

                        {tokenChartExpanded
                            ?
                                <Stack
                                    gap={2.5}
                                    sx={{
                                        mt: 0.75,
                                        px: '1rem',
                                        pt: '8px',
                                        pb: '12px',
                                    }}
                                >
                                    {tokenTags.map((tag, index) => (
                                        <Stack
                                            sx={{
                                                backgroundColor: 'rgba(51, 51, 51, 0.575)',
                                                borderRadius: '4px',
                                                boxShadow: '0 1px 4px 0 rgba(0, 0, 0, 0.20), 0 2px 6px 0 rgba(0, 0, 0, 0.20)',
                                            }}
                                        >
                                            <Typography
                                                key={index}
                                                className={`${tag.bgColorClass}`}
                                                onClick={() => handleTagClick(tag)}
                                                flexGrow={1}
                                                sx={{
                                                    fontSize: '14px',
                                                    fontWeight: 'bold',
                                                    letterSpacing: '0.9px',
                                                    color: '#fff',
                                                    borderRadius: '4px 4px 0 0',
                                                    mb: '5px',
                                                    pl: '8px',
                                                    py: '4px',
                                                }}
                                            >
                                                {tag.label}
                                            </Typography>

                                            <Box
                                                sx={{
                                                    mt: '6px'
                                                }}
                                            >
                                                <Typography
                                                    sx={{
                                                        //textAlign: 'center',
                                                        fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
                                                        fontSize: '12px',
                                                        letterSpacing: '0.04em',
                                                        color: colors.gray60,
                                                        pl: '9px',
                                                        borderBottom: '1px solid #808080'
                                                    }}
                                                >
                                                    The basics
                                                </Typography>
                                            </Box>

                                            <Stack
                                                gap={1.5}
                                                sx={{
                                                    p: '8px 10px 8px 10px',
                                                }}
                                            >
                                                {tag.theBasicsEntries.map((entry) => (
                                                    <Box
                                                        key={entry.id}
                                                        display="flex"
                                                    >
                                                        <BoxText
                                                            className={[
                                                                entry.isExample && 'tag-entry-example',
                                                                entry.isInsight && 'tag-entry-insight',
                                                            ]
                                                                .filter(Boolean)
                                                                .join(' ')
                                                            }
                                                        >
                                                            {entry.segments.map((segment) => (
                                                                <Box
                                                                    key={segment.id}
                                                                    component={segment.ref?.url ? 'a' : 'span'}
                                                                    href={segment.ref?.url}
                                                                    target="_blank"
                                                                    rel="noopener noreferrer"
                                                                    className={segment.isKeyword ? `${segment.highlightColor}` : ''}
                                                                    sx={{
                                                                        fontWeight: segment.isBold ? 700 : 'inherit',
                                                                        fontStyle: segment.isItalic ? 'italic' : 'inherit',
                                                                        fontFamily: segment.isCode
                                                                            ? "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace"
                                                                            : 'inherit',
                                                                        display: 'inline'
                                                                    }}
                                                                >
                                                                    {segment.text}
                                                                </Box>
                                                            ))}
                                                        </BoxText>
                                                    </Box>
                                                ))}
                                            </Stack>

                                            {tag.keyPointsEntries.length > 0 && (
                                                <Box
                                                    sx={{
                                                        mt: '0.5rem'
                                                    }}
                                                >
                                                    <Typography
                                                        sx={{
                                                            //textAlign: 'center',
                                                            fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
                                                            fontSize: '12px',
                                                            letterSpacing: '0.04em',
                                                            color: colors.gray60,
                                                            pl: '9px',
                                                            borderBottom: '1px solid #808080'
                                                        }}
                                                    >
                                                        Key points
                                                    </Typography>

                                                    <Stack
                                                        gap={1.5}
                                                        sx={{
                                                            p: '8px 10px 12px 10px',
                                                        }}
                                                    >
                                                        {tag.keyPointsEntries.map((entry) => (
                                                            <Box
                                                                key={entry.id}
                                                                display="flex"
                                                                gap={1}
                                                            >
                                                                <KeyIcon
                                                                    fontSize='small'
                                                                    sx={{
                                                                        color: '#ac7339'
                                                                    }}
                                                                />
                                                                <BoxText
                                                                    className={[
                                                                        entry.isExample && 'tag-entry-example',
                                                                        entry.isInsight && 'tag-entry-insight',
                                                                    ]
                                                                        .filter(Boolean)
                                                                        .join(' ')
                                                                    }
                                                                >
                                                                    {entry.segments.map((segment) => (
                                                                        <Box
                                                                            key={segment.id}
                                                                            component={segment.ref?.url ? 'a' : 'span'}
                                                                            href={segment.ref?.url}
                                                                            target="_blank"
                                                                            rel="noopener noreferrer"
                                                                            className={[
                                                                                segment.isKeyword && segment.highlightColor,
                                                                                segment.isCode && 'tag-code-segment',
                                                                                segment.ref && 'tag-link-segment',
                                                                            ]
                                                                                .filter(Boolean)
                                                                                .join(' ')
                                                                            }
                                                                            sx={{
                                                                                fontWeight: segment.isBold ? 700 : 'inherit',
                                                                                fontStyle: segment.isItalic ? 'italic' : 'inherit',
                                                                                fontFamily: segment.isCode ? "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace" : 'inherit',
                                                                                display: 'inline',
                                                                            }}
                                                                        >
                                                                            {segment.text}
                                                                        </Box>
                                                                    ))}
                                                                </BoxText>
                                                            </Box>
                                                        ))}
                                                    </Stack>
                                                </Box>
                                            )}

                                            {tag.useForEntries.length > 0 && (
                                                <Box
                                                    sx={{
                                                        mt: '0.5rem'
                                                    }}
                                                >
                                                    <Typography
                                                        sx={{
                                                            fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
                                                            fontSize: '12px',
                                                            letterSpacing: '0.04em',
                                                            color: colors.gray60,
                                                            pl: '9px',
                                                            borderBottom: '1px solid #808080'
                                                        }}
                                                    >
                                                        Use for
                                                    </Typography>

                                                    <Stack
                                                        gap={1.5}
                                                        sx={{
                                                            p: '8px 10px 12px 10px',
                                                        }}
                                                    >
                                                        {tag.useForEntries.map((entry) => (
                                                            <Box
                                                                key={entry.id}
                                                                display="flex"
                                                                gap={1}
                                                            >
                                                                <CodeIcon
                                                                    fontSize='small'
                                                                    sx={{
                                                                        color: '#00FF41',
                                                                    }}
                                                                />
                                                                <BoxText
                                                                    className={[
                                                                        entry.isExample && 'tag-entry-example',
                                                                        entry.isInsight && 'tag-entry-insight',
                                                                    ]
                                                                        .filter(Boolean)
                                                                        .join(' ')
                                                                    }
                                                                >
                                                                    {entry.segments.map((segment) => (
                                                                        <Box
                                                                            key={segment.id}
                                                                            component={segment.ref?.url ? 'a' : 'span'}
                                                                            href={segment.ref?.url}
                                                                            target="_blank"
                                                                            rel="noopener noreferrer"
                                                                            className={[
                                                                                segment.isKeyword && segment.highlightColor,
                                                                                segment.isCode && 'tag-code-segment',
                                                                                segment.ref && 'tag-link-segment',
                                                                            ]
                                                                                .filter(Boolean)
                                                                                .join(' ')
                                                                            }
                                                                            sx={{
                                                                                fontWeight: segment.isBold ? 700 : 'inherit',
                                                                                fontStyle: segment.isItalic ? 'italic' : 'inherit',
                                                                                fontFamily: segment.isCode ? "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace" : 'inherit',
                                                                                display: 'inline',
                                                                            }}
                                                                        >
                                                                            {segment.text}
                                                                        </Box>
                                                                    ))}
                                                                </BoxText>
                                                            </Box>
                                                        ))}
                                                    </Stack>
                                                </Box>
                                            )}

                                            {tag.exploreEntries.length > 0 && (
                                                <Box
                                                    sx={{
                                                        mt: '0.5rem'
                                                    }}
                                                >
                                                    <Typography
                                                        sx={{
                                                            //textAlign: 'center',
                                                            fontFamily: "'Roboto','Helvetica','Arial',sans-serif",
                                                            fontSize: '12px',
                                                            letterSpacing: '0.04em',
                                                            color: colors.gray60,
                                                            pl: '9px',
                                                            borderBottom: '1px solid #808080'
                                                        }}
                                                    >
                                                        Explore
                                                    </Typography>

                                                    <Stack
                                                        gap={1.5}
                                                        sx={{
                                                            py: '8px',
                                                            px: '10px'
                                                        }}
                                                    >
                                                        {tag.exploreEntries.map((entry) => (
                                                            <Box
                                                                key={entry.id}
                                                                display="flex"
                                                                gap={1}
                                                            >
                                                                {/* <ExploreOutlinedIcon
                                                                    //fontSize='small'
                                                                    sx={{
                                                                        color: '#00e6cf',
                                                                        fontSize : '16px',
                                                                        mt: '2px'
                                                                    }}
                                                                /> */}
                                                                <LinkIcon
                                                                    fontSize='small'
                                                                    sx={{
                                                                        color: '#00e6cf',
                                                                        // fontSize : '16px',
                                                                        // mt: '2px'
                                                                    }}
                                                                />
                                                                <BoxText>
                                                                    {entry.segments.map((segment) => (
                                                                        <Box
                                                                            gap={1}
                                                                            key={segment.id}
                                                                            component={segment.ref?.url ? 'a' : 'span'}
                                                                            href={segment.ref?.url}
                                                                            target="_blank"
                                                                            rel="noopener noreferrer"
                                                                            className={[
                                                                                segment.isKeyword && segment.highlightColor,
                                                                                segment.isCode && 'tag-code-segment',
                                                                                segment.ref && 'tag-link-segment',
                                                                            ]
                                                                                .filter(Boolean)
                                                                                .join(' ')
                                                                            }
                                                                            sx={{
                                                                                display: 'inline',
                                                                                fontWeight: segment.isBold ? 700 : 'inherit',
                                                                                fontStyle: segment.isItalic ? 'italic' : 'inherit',
                                                                                fontFamily: segment.isCode
                                                                                    ? "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace"
                                                                                    : 'inherit',
                                                                                backgroundColor: segment.highlightColor ?? 'transparent',
                                                                                display: 'inline'
                                                                            }}
                                                                        >
                                                                            {segment.text}
                                                                        </Box>
                                                                    ))}
                                                                </BoxText>
                                                            </Box>
                                                        ))}
                                                    </Stack>
                                                </Box>
                                            )}
                                            
                                        </Stack>
                                    ))}
                                </Stack>
                            :
                                <></>
                        }

                    </Stack>

                    <Stack>
                        <Stack>
                            <Divider sx={{ bgcolor: '#808080' }} />
                            
                                <Box
                                    display="flex"
                                    alignItems="center"
                                >
                                    <Tooltip title="The uploaded C# source code has been parsed into a syntax tree using the Micrsoft.CodeAnalysis.CSharp (&quot;Roslyn&quot;) library. This syntax tree is then parsed to create the &quot;navigation&quot; tokens displayed on the screen. The following charts provide facts & insights on each token's ancestor nodes in the syntax tree. Click the compass icon to highlight the ancestor tokens in the source code.">
                                        <Box
                                            display="flex"
                                            alignItems="center"
                                            sx={{
                                                pt: 1,
                                                pl: 1.15
                                            }}
                                        >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" fill="currentColor" class="bi bi-map bootstrap-icon-fill-gray" viewBox="0 0 16 16">
                                                <path fill-rule="evenodd" d="M15.817.113A.5.5 0 0 1 16 .5v14a.5.5 0 0 1-.402.49l-5 1a.5.5 0 0 1-.196 0L5.5 15.01l-4.902.98A.5.5 0 0 1 0 15.5v-14a.5.5 0 0 1 .402-.49l5-1a.5.5 0 0 1 .196 0L10.5.99l4.902-.98a.5.5 0 0 1 .415.103M10 1.91l-4-.8v12.98l4 .8zm1 12.98 4-.8V1.11l-4 .8zm-6-.8V1.11l-4 .8v12.98z"/>
                                            </svg>
                                            <Typography
                                                className='code'
                                                sx={{
                                                    px: 1,
                                                    color: colors.gray60,
                                                    fontSize: '14px'
                                                }}
                                            >
                                                Ancestor Charts
                                            </Typography>
                                        </Box>
                                    </Tooltip>
                                </Box>

                            {charts.map((chart, index) => (
                                <Stack
                                    className='chart-border-gray chart-bg-gray'
                                    sx={{
                                        mt: 2,
                                        boxShadow: '0 1px 4px 0 rgba(0, 0, 0, 0.20), 0 2px 6px 0 rgba(0, 0, 0, 0.20)',
                                        borderRadius: '4px',
                                        mx: 2,
                                        px: '12px',
                                        py: '8px',
                                    }}
                                >
                                    <Box
                                        display="flex"
                                        justifyContent="flex-start"
                                        alignItems="center"
                                        gap={1.25}
                                        sx={{
                                            mb: chart == activeChart ? '0.75rem' : '0'
                                        }}
                                    >
                                        <IconButton
                                            onClick={() => toggleChartHighlighting(chart)}
                                            sx={{
                                                p: 0,
                                                m: 0
                                            }}
                                        >
                                            { activeHighlightIndices === chart.highlightIndices
                                                ?
                                                    <ExploreIcon
                                                        fontSize='small'
                                                        sx={{
                                                            color: colors.orange
                                                        }}
                                                    />
                                                :
                                                    <ExploreIcon
                                                        fontSize='small'
                                                        sx={{
                                                            color: colors.gray45
                                                        }}
                                                    />
                                            }
                                        </IconButton>
                                        <Typography
                                            key={index}
                                            className='code'
                                            flexGrow={1}
                                            onClick={() => handleExpandChartClick(chart)}
                                            sx={{
                                                fontSize: '14px',
                                                cursor: 'pointer',
                                                color: colors.gray90,
                                                borderRadius: '4px',
                                            }}
                                        >
                                            {chart.label}
                                        </Typography>
                                        <IconButton
                                            onClick={() => handleExpandChartClick(chart)}
                                            sx={{
                                                p: 0,
                                                m: 0
                                            }}
                                        >
                                            <ExpandMoreIcon
                                                fontSize='small'
                                                sx={{
                                                    color: colors.white
                                                }}
                                            />
                                        </IconButton>
                                    </Box>
                                    { activeChart === chart
                                        ?
                                            <Stack
                                                gap={1}
                                            >
                                                {chart.facts.length > 0
                                                    ?
                                                        <GrayBox>
                                                            <Stack
                                                                gap={2}
                                                            >
                                                                {chart.alias !== null
                                                                    ?
                                                                        <Box
                                                                            display="flex"
                                                                            alignItems="baseline"
                                                                            gap={1.75}
                                                                        >
                                                                            <Box>
                                                                                <Tooltip title="Also called">
                                                                                <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" className="bi bi-layers bootstrap-icon-fill" viewBox="0 0 16 16">
                                                                                    <path d="M8.235 1.559a.5.5 0 0 0-.47 0l-7.5 4a.5.5 0 0 0 0 .882L3.188 8 .264 9.559a.5.5 0 0 0 0 .882l7.5 4a.5.5 0 0 0 .47 0l7.5-4a.5.5 0 0 0 0-.882L12.813 8l2.922-1.559a.5.5 0 0 0 0-.882zm3.515 7.008L14.438 10 8 13.433 1.562 10 4.25 8.567l3.515 1.874a.5.5 0 0 0 .47 0zM8 9.433 1.562 6 8 2.567 14.438 6z"/>
                                                                                </svg>
                                                                                </Tooltip>
                                                                            </Box>
                                                                            <BoxText className='code'>
                                                                                {chart.alias}
                                                                            </BoxText>
                                                                        </Box>
                                                                    :
                                                                    <></>
                                                                }

                                                                {chart.facts.map((fact, index) => (
                                                                    <Box
                                                                        display="flex"
                                                                        alignItems="baseline"
                                                                        gap={1.75}
                                                                    >
                                                                        <Box>
                                                                            <Tooltip title="Fact">
                                                                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" className="bi bi-book bootstrap-icon-fill" viewBox="0 0 16 16">
                                                                                    <path d="M1 2.828c.885-.37 2.154-.769 3.388-.893 1.33-.134 2.458.063 3.112.752v9.746c-.935-.53-2.12-.603-3.213-.493-1.18.12-2.37.461-3.287.811zm7.5-.141c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783"/>
                                                                                </svg>
                                                                            </Tooltip>
                                                                        </Box>
                                                                        <BoxText
                                                                            key={index}
                                                                        >
                                                                            {fact}
                                                                        </BoxText>
                                                                    </Box>
                                                                ))}
                                                            </Stack>
                                                        </GrayBox>
                                                    :
                                                        <></>
                                                }

                                                {chart.insights.length > 0
                                                    ?
                                                        <OrangeBox>
                                                            <Stack
                                                                gap={2}
                                                            >
                                                                {chart.insights.map((insight, index) => (
                                                                    <Box
                                                                        display="flex"
                                                                        alignItems="baseline"
                                                                        gap={1.75}
                                                                    >
                                                                        <Box>
                                                                            <Tooltip title="Insight">
                                                                                <svg xmlns="http://www.w3.org/2000/svg" width="13" height="13" className="bi bi-compass bootstrap-icon-fill" viewBox="0 0 16 16">
                                                                                    <path d="M8 16.016a7.5 7.5 0 0 0 1.962-14.74A1 1 0 0 0 9 0H7a1 1 0 0 0-.962 1.276A7.5 7.5 0 0 0 8 16.016m6.5-7.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0"/>
                                                                                    <path d="m6.94 7.44 4.95-2.83-2.83 4.95-4.949 2.83 2.828-4.95z"/>
                                                                                </svg>
                                                                            </Tooltip>
                                                                        </Box>
                                                                        <BoxText
                                                                            key={index}
                                                                        >
                                                                            {insight}
                                                                        </BoxText>
                                                                    </Box>
                                                                ))}
                                                            </Stack>
                                                        </OrangeBox>
                                                    :
                                                        <></>
                                                }
                                            </Stack>
                                        :
                                            <></>
                                    }
                                </Stack>
                            ))}
                        </Stack>
                    </Stack>
                </Stack>
            </Stack>
        </ContentContainer>
    );
}

export default TokenSidebarContent;