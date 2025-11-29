import React, { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Typography } from '@mui/material';

const CodeContainer = styled(Box)(() => ({
    display: 'flex',
    paddingTop: '7rem',
    // height: '100%',
    // width: '55%',
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    paddingRight: '8rem',
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

const StagingCodeContent = ({ leftSideBarOpen, navTokens, activeToken, setActiveToken }) => {

    // Constants
    // Find the index of the activeToken in the tokenList
    const activeTokenIndex = navTokens.findIndex(token => token === activeToken);

    // State Variables
    const [selectedTokenIndex, setSelectedTokenIndex] = useState(null);
    const [selectedTokenIndexList, setSelectedTokenIndexList] = useState([]);
    const [isDragging, setIsDragging] = useState(false);

    // Event Handlers
    const handleMouseDown = (index) => {
        setIsDragging(true);
        // Check if the token is already selected; if yes, deselect it; otherwise, select it.
        if (!isDragging) {
            setSelectedTokenIndexList((prevList) => {
                if (prevList.includes(index)) {
                    return prevList.filter(i => i !== index);
                } else {
                    return [...prevList, index];
                }
            });
        }
    };

    const handleMouseEnter = (index) => {
        if (isDragging) {
            setSelectedTokenIndexList((prevList) => {
                if (!prevList.includes(index)) {
                    return [...prevList, index];
                }
                return prevList;
            });
        }
    };

    const handleMouseUp = () => {
        setIsDragging(false);
    };

    const handleClick = (index) => {
        setSelectedTokenIndex(index);
        setActiveToken(navTokens[index]);
    };

    // Use Effects
    // useEffect(() => {
        
    // }, []);

    // API Calls

    // Common styling configuration
    const codeStyle = {
        fontFamily: 'Consolas, Input, DejaVu Sans Mono',
        fontSize: '17px',
        lineHeight: '23px',
        marginLeft: leftSideBarOpen ? '25rem' : '4.5rem',
    };

    const lineNumbers = [];
    let currentLine = 1;

    // Generate line numbers based on tokenList
    navTokens.forEach(token => {

        // Check for newlines in the leading and trailing trivia
        const newlines = token.leadingTrivia.filter(trivia => trivia === "\r\n").length + 
                        token.trailingTrivia.filter(trivia => trivia === "\r\n").length;

        // Increment current line for each newline
        currentLine += newlines;
        lineNumbers.push(currentLine);
    });

    return (
        <CodeContainer>
            <FlexBox
                onMouseUp={handleMouseUp} // Ensure mouse up is handled at the FlexBox level
                className="disable-text-selection" // Disable text selection
            >
                <FlexBox
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
                        {navTokens.map((token, index) => {
                        
                        // Determine the color class based on the TokenColor value
                        const colorClass = token.highlightColor
                            ? `${token.highlightColor}`
                            : 'color-white';

                        // Determine if this token is selected
                        const isSelected = selectedTokenIndex === index;

                        // Combine classes, but only add "token-hover", "selected", and "dev-insight" if applicable
                        const highlightColor = `pad-token code ${colorClass} ${isSelected ? 'token-active' : 'token-hover'}`;

                        // Helper function to render trivia
                        const renderTrivia = (trivia) => {
                            return trivia.map((triviaItem, triviaIndex) => {
                                if (triviaItem === '\r\n') {
                                    return <br key={`trivia-${index}-${triviaIndex}`} />;
                                }
                        
                                // If the trivia contains only one space, render a single &nbsp;
                                if (triviaItem === ' ') {
                                    return <span key={`trivia-${index}-${triviaIndex}`}>&nbsp;</span>;
                                }
                        
                                // If the trivia contains multiple spaces, render them inside a span with "pad-token code"
                                if (triviaItem.includes(' ')) {
                                    const spaceCount = triviaItem.length; // Double the spaces as requested
                                    return (
                                        <span key={`trivia-${index}-${triviaIndex}`} className="pad-token code">
                                            {Array(spaceCount).fill('\u00A0')}
                                        </span>
                                    );
                                }
                        
                                return triviaItem; // Default rendering for non-space trivia
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
                </FlexBox>
            </FlexBox>
        </CodeContainer>
    );
}

export default StagingCodeContent;