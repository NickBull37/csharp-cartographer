import { useState, useEffect } from 'react';
import { styled } from '@mui/material/styles';
import { Box, Typography } from '@mui/material';

const CodeContainer = styled(Box)(() => ({
    display: 'flex',
    paddingTop: '7.75rem',
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

const CodeWindow = ({
    leftSidebarOpen,
    tokenList,
    activeToken,
    setActiveToken,
    // activeHighlightIndices,
    // setActiveHighlightIndices,
    activeHighlightRange,
    setActiveHighlightRange,
    selectedTokens,
    setSelectedTokens
}) => {

    // Constants
    const activeTokenIndex = tokenList.findIndex(token => token === activeToken);

    // State Variables
    const [selectModeActive, setSelectModeActive] = useState(true);
    const [highlightModeActive, setHighlightModeActive] = useState(false);
    const [selectedTokenIndex, setSelectedTokenIndex] = useState(null);

    const isInActiveHighlightRange = (tokenIndex) => {
        if (!activeHighlightRange) return false;

        const start = activeHighlightRange.startIndex;
        const end = activeHighlightRange.endIndex;

        // Guard in case start/end are null/undefined
        if (start === undefined || end === undefined || start === null || end === null) return false;

        const min = Math.min(start, end);
        const max = Math.max(start, end);

        return tokenIndex >= min && tokenIndex <= max;
    };

    // highlighting tokens
    const [isDragging, setIsDragging] = useState(false);

    // Event Handlers
    const handleClick = (index) => {
        if (selectModeActive) {
            setSelectedTokenIndex(index);
            setActiveToken(tokenList[index]);
        }
    };

    const handleMouseDown = (index) => {
        if (highlightModeActive) {
            setIsDragging(true);
            // Check if the token is already selected; if yes, deselect it; otherwise, select it.
            if (!isDragging) {
                setSelectedTokens((prevSelectedTokens) => {
                    if (prevSelectedTokens.includes(index)) {
                        return prevSelectedTokens.filter(i => i !== index);
                    } else {
                        return [...prevSelectedTokens, index];
                    }
                });
            }
        }
    };

    const handleMouseEnter = (index) => {
        if (highlightModeActive) {
            if (isDragging) {
                setSelectedTokens((prevSelectedTokens) => {
                    if (!prevSelectedTokens.includes(index)) {
                        return [...prevSelectedTokens, index];
                    }
                    return prevSelectedTokens;
                });
            }
        }
    };

    const handleMouseUp = () => {
        if (highlightModeActive) {
            setIsDragging(false);
        }
    };

    // Use Effects
    useEffect(() => {
        setSelectedTokenIndex(activeTokenIndex);
    }, [activeToken]);

    useEffect(() => {
        if (leftSidebarOpen) {
            setHighlightModeActive(true);
            setSelectModeActive(false);
        }
        else {
            setSelectModeActive(true);
            setHighlightModeActive(false);
        }
    }, [leftSidebarOpen]);

    // Common styling configuration
    const codeStyle = {
        fontFamily: 'Consolas, Input, DejaVu Sans Mono',
        fontSize: '17px',
        lineHeight: '23px',
        marginLeft: leftSidebarOpen ? '25rem' : '4.5rem',
    };

    const lineNumbers = [];
    let currentLineNumber = 1;

    // Generate line numbers based on tokenList
    tokenList.forEach(currentToken => {
        // Check for newlines in the leading and trailing trivia
        const newlineCount = currentToken.leadingTrivia
            .filter(trivia => trivia === "\r\n")
            .length + 
            currentToken.trailingTrivia.filter(trivia => trivia === "\r\n").length;
    
        // Calculate the new line number
        const previousLineNumber = currentLineNumber;
        currentLineNumber += newlineCount;
    
        // Fill in any missing line numbers
        for (let line = previousLineNumber + 1; line < currentLineNumber; line++) {
            lineNumbers.push(line);
        }
    
        // Add the current line number
        lineNumbers.push(currentLineNumber);
    });
    

    return (
        <CodeContainer
            className="disable-text-selection"
            onMouseUp={handleMouseUp}
        >
            <FlexBox
                sx={{
                    ...codeStyle
                }}
            >
                <LineNumberBox>
                    {lineNumbers.map((lineNumber, index) => (
                        <LineNumberText
                            key={index}
                            sx={{
                                lineHeight: 'inherit',
                                fontSize: 'inherit'
                            }}
                        >
                            {index === 0 || lineNumbers[index] !== lineNumbers[index - 1] ? lineNumber : ''}
                        </LineNumberText>
                    ))}
                </LineNumberBox>
                <Typography
                    sx={{
                        lineHeight: 'inherit',
                        fontSize: '1rem'
                    }}
                >
                    {tokenList.map((token, index) => {
                        
                        // Determine the color class based on the TokenColor value
                        const colorClass = token.highlightColor
                            ? `${token.highlightColor}`
                            : 'color-white';

                        const isAiHighlight = selectedTokens.includes(index);

                        // Determine if this token is selected
                        const isSelected = selectedTokenIndex === index;
                        const isTagHighlight = isInActiveHighlightRange(token.index);

                        // Combine classes, but only add "token-hover", "selected", and "dev-insight" if applicable
                        const highlightColor = `pad-token code ${colorClass} ${isSelected ? 'token-active' : 'token-hover'} ${isTagHighlight ? 'tag-highlight' : ''} ${isAiHighlight ? 'dev-highlight' : ''}`;

                        // Helper function to render trivia
                        const renderTrivia = (trivia, index) => {
                            return trivia.map((triviaItem, triviaIndex) => {

                                let triviaClass = "pad-token code"; // Default class

                                // Check the first few characters of the trivia item and apply different classes
                                if (triviaItem.startsWith("#")) {
                                    triviaClass += " color-dark-gray";
                                } else if (triviaItem.startsWith("//")) {
                                    triviaClass += " color-dark-green";
                                } else if (triviaItem.startsWith("///")) {
                                    triviaClass += " color-dark-green";
                                } else if (triviaItem.startsWith("/*")) {
                                    triviaClass += " color-dark-green";
                                } else if (triviaItem.startsWith("*")) {
                                    triviaClass += " color-dark-green";
                                }

                                // render new line trivia
                                if (triviaItem === '\r\n') {
                                    return <br key={`trivia-${index}-${triviaIndex}`} />;
                                }
                        
                                const isTagHighlight = isInActiveHighlightRange(token.index);

                                // render single space trivia
                                if (triviaItem === ' ' && !isTagHighlight) {
                                    return <span key={`trivia-${index}-${triviaIndex}`}>&nbsp;</span>;
                                }
                                else if (triviaItem === ' ' && isTagHighlight) {
                                    return <span key={`trivia-${index}-${triviaIndex}`} className="tag-highlight">&nbsp;</span>;
                                }
                        
                                // render multi-space trivia as single block and double size
                                if (/^\s+$/.test(triviaItem)) {
                                    const spaceCount = triviaItem.length;
                                    return (
                                        <span key={`trivia-${triviaIndex}`} className="pad-token code">
                                            {Array(spaceCount).fill('\u00A0')}
                                        </span>
                                    );
                                }
                        
                                // render text trivia
                                return (
                                    <span key={`trivia-${triviaIndex}`} className={triviaClass}>
                                        {triviaItem}
                                    </span>
                                );
                            });
                        };
                        
                        return (
                            <span key={index}>
                                {/* Render leading trivia */}
                                {renderTrivia(token.leadingTrivia, token.index)}

                                {/* Render the token itself */}
                                <span 
                                    className={highlightColor} 
                                    onClick={() => handleClick(index)}
                                    onMouseDown={() => handleMouseDown(index)}
                                    onMouseEnter={() => handleMouseEnter(index)}
                                >
                                    {token.text.replace(/ /g, '\u00A0')}
                                </span>

                                {/* Render trailing trivia */}
                                {renderTrivia(token.trailingTrivia, token.index)}
                            </span>
                        );
                    })}
                </Typography>
            </FlexBox>
        </CodeContainer>
    );
}

export default CodeWindow;