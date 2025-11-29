import React, { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Typography, Tooltip } from '@mui/material';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    height: '100%',
    width: '55%',
}));

const LineNumberBox = styled(Box)(() => ({
    paddingRight: '16px',
    marginRight: '16px',
    borderRight: '1px solid #808080',
    textAlign: 'right',
    color: '#808080',
}));

const LineNumberText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '17px',
    lineHeight: '23px'
}));

const TokenMapper = ({ tokenList, activeToken, setActiveToken, insightHighlightIndexes, setInsightHighlightIndexes }) => {

    // Constants
    // Find the index of the activeToken in the tokenList
    const activeTokenIndex = tokenList.findIndex(token => token === activeToken);

    // State Variables
    const [selectedTokenIndex, setSelectedTokenIndex] = useState(null);

    // Event Handlers
    const handleClick = (index) => {
        setInsightHighlightIndexes([]);
        setSelectedTokenIndex(index);
        setActiveToken(tokenList[index]);
    };

    // Use Effects
    useEffect(() => {
        setSelectedTokenIndex(activeTokenIndex);
    }, [activeToken]);

    useEffect(() => {
        if (tokenList && tokenList.length > 0) {
            // tokenList.forEach(token => {
            //     console.log(token);
            // });
        }
    }, [tokenList]);

    // API Calls

    // Common styling configuration
    const codeStyle = {
        fontFamily: 'Consolas, Input, DejaVu Sans Mono',
        fontSize: '17px',
        lineHeight: '23px',
    };

    const lineNumbers = [];
    let currentLine = 1;

    tokenList.forEach(token => {

        // Check for newlines in the leading and trailing trivia
        const newlines = token.leadingTrivia.filter(trivia => trivia === "\r\n").length + 
                        token.trailingTrivia.filter(trivia => trivia === "\r\n").length;

        // Increment current line for each newline
        currentLine += newlines;
        lineNumbers.push(currentLine);
    });

    return (
        <FlexBox>
        <Box
            display="flex"
            sx={{
                ...codeStyle
            }}
        >
            <LineNumberBox>
                {lineNumbers.map((line, index) => (
                    <LineNumberText
                        key={index}
                        sx={{
                            lineHeight: 'inherit',
                            fontSize: 'inherit'
                        }}
                    >
                        {index === 0 || lineNumbers[index] !== lineNumbers[index - 1] ? line : ''}
                    </LineNumberText>
                ))}
            </LineNumberBox>
            <Typography
                sx={{
                    lineHeight: 'inherit',
                    fontSize: 'inherit',
                }}
            >
                {tokenList.map((token, index) => {
                    // Determine the color class based on the TokenColor value
                    const colorClass = token.highlightColor
                        ? `${token.highlightColor}`
                        : 'color-white';

                    // Determine if this token is selected
                    const isSelected = selectedTokenIndex === index;

                    // Determine if this token should have the "dev-insight" class
                    const hasInsightHighlight = insightHighlightIndexes.includes(index);

                    // Combine classes, but only add "token-hover", "selected", and "dev-insight" if applicable
                    const highlightColor = `pad-token code ${colorClass} ${isSelected ? 'token-active' : 'token-hover'} ${hasInsightHighlight ? 'dev-insight' : ''}`;

                    // Helper function to render trivia
                    const renderTrivia = (trivia) => {
                        return trivia.map((triviaItem, triviaIndex) => {
                            if (triviaItem === '\r\n') {
                                return <br key={`trivia-${index}-${triviaIndex}`} />;
                            }
                            if (triviaItem === ' ') {
                                return <span key={`trivia-${index}-${triviaIndex}`}>&nbsp;</span>;
                            }
                            return triviaItem;
                        });
                    };

                    return (
                        <span key={index}>
                            {/* Render leading trivia */}
                            {renderTrivia(token.leadingTrivia)}

                            {/* Render the token itself */}
                            <span 
                                className={highlightColor} 
                                onClick={() => handleClick(index)}
                            >
                                {token.text.replace(/ /g, '\u00A0')}
                            </span>

                            {/* Render trailing trivia */}
                            {renderTrivia(token.trailingTrivia)}
                        </span>
                    );
                })}
            </Typography>
        </Box>
    </FlexBox>
        // <FlexBox>
        //     <Box
        //         display="flex"
        //         sx={{
        //             ...codeStyle
        //         }}
        //     >
        //         <LineNumberBox>
        //             {lineNumbers.map((line, index) => (
        //                 <LineNumberText
        //                     key={index}
        //                     sx={{
        //                         lineHeight: 'inherit',
        //                         fontSize: 'inherit'
        //                     }}
        //                 >
        //                     {index === 0 || lineNumbers[index] !== lineNumbers[index - 1] ? line : ''}
        //                 </LineNumberText>
        //             ))}
        //         </LineNumberBox>
        //         <Typography
        //             sx={{
        //                 lineHeight: 'inherit',
        //                 fontSize: 'inherit',
        //             }}
        //         >
        //             {tokenList.map((token, index) => {

        //                 // Determine the color class based on the TokenColor value
        //                 const colorClass = token.highlightColor
        //                     ? `${token.highlightColor}`
        //                     : 'color-white';

        //                 // Determine if this token is selected
        //                 const isSelected = selectedTokenIndex === index;

        //                 // Determine if this token should have the "dev-insight" class
        //                 const hasInsightHighlight = insightHighlightIndexes.includes(index);

        //                 // Combine classes, but only add "token-hover", "selected", and "dev-insight" if applicable
        //                 const highlightColor = token.text !== '<newline>' && token.text.trim() !== ''
        //                 ? `pad-token code ${colorClass} ${isSelected ? 'token-active' : 'token-hover'} ${hasInsightHighlight ? 'dev-insight' : ''}`
        //                 : `pad-token code ${colorClass}`;

        //                 return (
        //                     token.text === '<newline>' ? <br key={index} /> :
        //                     token.text === ' ' ? <span key={index}>&nbsp;</span> :
        //                     <span 
        //                         className={highlightColor} 
        //                         key={index} 
        //                         onClick={() => handleClick(index)}
        //                     >
        //                         {token.text.replace(/ /g, '\u00A0')}
        //                     </span>
        //                 );
        //             })}
        //         </Typography>
        //     </Box>
        // </FlexBox>
    );
}

export default TokenMapper;