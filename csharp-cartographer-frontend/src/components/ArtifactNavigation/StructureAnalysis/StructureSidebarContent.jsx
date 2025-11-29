import React, { useState, useEffect } from 'react';
import axios from "axios";
import { styled } from '@mui/material/styles';
import { Box, Stack, Typography } from '@mui/material';
import { Accordion, AccordionSummary, AccordionDetails } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import colors from '../../../utils/colors';

const FlexBox = styled(Box)(() => ({
    display: 'flex',
}));

const ContentContainer = styled(Box)(() => ({
    display: 'flex',
    padding: '8px',
    height: '82%',
}));

const BlueAccordion = styled(Accordion)(() => ({
    backgroundColor: colors.gray25,
    border: '2px solid #235f90',
    color: '#fff',
    marginBottom: '1rem',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const BlueAccordionSummary = styled(AccordionSummary)(() => ({
    // fontFamily: '#000',
    backgroundColor: 'rgba(70, 147, 210, 0.5)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(70, 147, 210, 0.5)',
        borderBottom: '1px solid #235f90',
    },
}));

const GreenAccordion = styled(Accordion)(() => ({
    backgroundColor: '#595959',
    border: '2px solid #309c86',
    color: '#fff',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const GreenAccordionSummary = styled(AccordionSummary)(() => ({
    backgroundColor: 'rgba(60, 195, 168, 0.25)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(60, 195, 168, 0.25)',
        borderBottom: '1px solid #309c86',
    },
}));

const LightGreenAccordion = styled(Accordion)(() => ({
    backgroundColor: '#595959',
    border: '2px solid #5b9c30',
    color: '#fff',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const LightGreenAccordionSummary = styled(AccordionSummary)(() => ({
    backgroundColor: 'rgba(128, 201, 79, 0.35)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(128, 201, 79, 0.3)',
        borderBottom: '1px solid #5b9c30',
    },
}));

const YellowAccordion = styled(Accordion)(() => ({
    backgroundColor: '#595959',
    border: '2px solid #cbcb80',
    color: '#fff',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const YellowAccordionSummary = styled(AccordionSummary)(() => ({
    backgroundColor: 'rgba(220, 220, 170, 0.3)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(220, 220, 170, 0.3)',
        borderBottom: '1px solid #cbcb80',
    },
}));

const RedAccordion = styled(Accordion)(() => ({
    backgroundColor: '#595959',
    border: '2px solid #ff6666',
    color: '#fff',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const RedAccordionSummary = styled(AccordionSummary)(() => ({
    backgroundColor: 'rgba(255, 102, 102, 0.25)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(255, 102, 102, 0.25)',
        borderBottom: '1px solid #ff6666',
    },
}));

const PurpleAccordion = styled(Accordion)(() => ({
    backgroundColor: '#595959',
    border: '2px solid #c266ff',
    color: '#fff',
    '&.Mui-expanded': {
        margin: '0',
    },
}));
const PurpleAccordionSummary = styled(AccordionSummary)(() => ({
    backgroundColor: 'rgba(194, 102, 255, 0.2)',
    '&.Mui-expanded': {
        minHeight: '42px', // Limit height to 30px when expanded
        maxHeight: '42px', // Ensure height does not exceed 30px
        backgroundColor: 'rgba(194, 102, 255, 0.2)',
        borderBottom: '1px solid #c266ff',
    },
}));

const StyledAccordionDetails = styled(AccordionDetails)(() => ({
    padding: '8px',
}));

const BulletText = styled(Typography)(() => ({
    fontFamily: "'Cascadia Code', 'Fira Code', 'Consolas', 'Courier New', monospace",
    fontSize: '0.875rem',
    color: colors.gray85,
}));

//ss_navigator._05.Services

const StructureSidebarContent = () => {

    const [blueBoxExpanded, setBlueBoxExpanded] = useState(false);
    const [greenBoxExpanded, setGreenBoxExpanded] = useState(false);
    const [yellowBoxExpanded, setYellowBoxExpanded] = useState(false);

    return (
        <ContentContainer>
            <div>
                <BlueAccordion>
                    <BlueAccordionSummary
                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                        aria-controls="panel1-content"
                        id="panel1-header"
                        className="text"
                    >
                        Namespace
                    </BlueAccordionSummary>
                    <StyledAccordionDetails>
                        <Stack
                            gap={0.5}
                            sx={{
                                mt: 1,
                                mb: 2
                            }}
                        >
                            <Typography
                                sx={{
                                    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
                                    //fontSize: '0.875rem',
                                    color: '#fff',
                                    mb: 0.75,
                                    textAlign: 'center'
                                }}
                            >
                                ss_navigator._05.Services
                            </Typography>
                            <BulletText>
                                Classes - [1]
                            </BulletText>
                            <BulletText>
                                Interfaces - [1]
                            </BulletText>
                            <Typography
                                variant='body2'
                                sx={{
                                    p: 0.75,
                                    mt: 1,
                                    // border: '1px solid #00e6cf',
                                    borderRadius: '4px',
                                    backgroundColor: colors.gray35,
                                }}
                            >
                                A namespace is like an address that other areas of the codebase can use to find these elements. When code in another area wants to reference these items, the file needs to have a using statement referencing this namespace.<br /><br />Any other code that exists in this namespace can reference these classes & interfaces freely. Any outside code that wants to reference the code inside will have to include a using statement referencing this namespace.
                            </Typography>
                        </Stack>
                        <Stack gap={1}>
                            <div>
                                <LightGreenAccordion>
                                    <LightGreenAccordionSummary
                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                        aria-controls="panel1-content"
                                        id="panel1-header"
                                        className="text"
                                    >
                                        IRoslynAnalyzer
                                    </LightGreenAccordionSummary>
                                    <StyledAccordionDetails>
                                        <Stack
                                            gap={0.5}
                                            sx={{
                                                mt: 1,
                                                mb: 2
                                            }}
                                        >
                                            <BulletText>
                                                Fields - [0]
                                            </BulletText>
                                            <BulletText>
                                                Properties - [0]
                                            </BulletText>
                                            <BulletText>
                                                Methods - [3]
                                            </BulletText>
                                            <Typography
                                                variant='body2'
                                                sx={{
                                                    p: 0.75,
                                                    mt: 1,
                                                    // border: '1px solid #00e6cf',
                                                    borderRadius: '4px',
                                                    backgroundColor: '#666666',
                                                }}
                                            >
                                                An interface defines a contract for any classes that inherit from it. Any fields or properties must have the same data types. Any methods must have the exact same method signature.<br /><br />This public interface can be referenced from anywhere. If a class inherits from this interface, that class must provide an implementation for the following methods.
                                            </Typography>
                                        </Stack>
                                        <Stack gap={1}>
                                            <div>
                                                <YellowAccordion>
                                                    <YellowAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        GenerateSyntaxTree
                                                    </YellowAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                async: 
                                                            </BulletText>
                                                            <BulletText>
                                                                access: <span className='code color-blue'>public</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                returns: <span className='code color-green'>SyntaxTree</span>
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This public method can be accessed from anywhere without restriction. The object calling this method must pass a SyntaxTree as an argument.<br /><br />This method is not static which means an object reference is required to use this method. The class calling this method must create a new() instance of this class to call the method with.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </YellowAccordion>
                                            </div>
                                            <div>
                                                <YellowAccordion>
                                                    <YellowAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        GenerateNavTokens
                                                    </YellowAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                async: 
                                                            </BulletText>
                                                            <BulletText>
                                                                access: <span className='code color-blue'>public</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                returns: <span className='code color-light-green'>IEnumerable</span>&lt;<span className='code color-green'>NavToken</span>&gt;
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This public method can be accessed from anywhere without restriction. It is not a static method so a object reference is needed to use this method.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </YellowAccordion>
                                            </div>
                                        </Stack>
                                    </StyledAccordionDetails>
                                </LightGreenAccordion>
                            </div>
                            <div>
                                <GreenAccordion>
                                    <GreenAccordionSummary
                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                        aria-controls="panel1-content"
                                        id="panel1-header"
                                        className="text"
                                    >
                                        RoslynAnalyzer
                                    </GreenAccordionSummary>
                                    <StyledAccordionDetails>
                                        <Stack
                                            gap={0.5}
                                            sx={{
                                                mt: 1,
                                                mb: 2
                                            }}
                                        >
                                            <BulletText>
                                                Inherits - <span className='code color-light-green'>IRoslynAnalyzer</span>
                                            </BulletText>
                                            <BulletText>
                                                Fields - [2]
                                            </BulletText>
                                            <BulletText>
                                                Properties - [0]
                                            </BulletText>
                                            <BulletText>
                                                Methods - [6]
                                            </BulletText>
                                            <Typography
                                                variant='body2'
                                                sx={{
                                                    p: 0.75,
                                                    mt: 1,
                                                    // border: '1px solid #00e6cf',
                                                    borderRadius: '4px',
                                                    backgroundColor: '#666666',
                                                }}
                                            >
                                                Any code existing in this namespace can reference these classes & interfaces freely. Any outside code that wants to reference the code inside will have to include a using statement referencing this namespace.
                                            </Typography>
                                        </Stack>
                                        <Stack gap={1}>
                                            <div>
                                                <RedAccordion>
                                                    <RedAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        _roslynPrimitiveKinds
                                                    </RedAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                access: <span className='code color-blue'>private</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                readonly: <b>Y</b>
                                                            </BulletText>
                                                            <BulletText>
                                                                type: <span className='code color-green'>List</span>&lt;<span className='code color-blue'>string</span>&gt;
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This private field can only be referenced from within this class. The readonly attribute makes the field immutable so the value can never change. It is initialized to a hardcoded value and does not need to be initialized in the constructor.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </RedAccordion>
                                            </div>
                                            <div>
                                                <PurpleAccordion>
                                                    <PurpleAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        _navInsightProvider
                                                    </PurpleAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                access: <span className='code color-blue'>private</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                readonly: <b>Y</b>
                                                            </BulletText>
                                                            <BulletText>
                                                                type: <span className='code color-light-green'>INavInsightProvider</span>
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This private field can only be referenced from within this class. The readonly attribute makes the field immutable so the value can never change. This field is not initialized when it is declared. In order to initialize this field with a value, a value must be passed into the constructor of the class when the class object reference is created.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </PurpleAccordion>
                                            </div>
                                            <div>
                                                <YellowAccordion>
                                                    <YellowAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        GenerateSyntaxTree
                                                    </YellowAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                async: 
                                                            </BulletText>
                                                            <BulletText>
                                                                access: <span className='code color-blue'>public</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                returns: <span className='code color-green'>SyntaxTree</span>
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This public method can be accessed from anywhere without restriction. It is not a static method so a object reference is needed to use this method.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </YellowAccordion>
                                            </div>
                                            <div>
                                                <YellowAccordion>
                                                    <YellowAccordionSummary
                                                        expandIcon={<ExpandMoreIcon sx={{ color: '#fff' }} />}
                                                        aria-controls="panel1-content"
                                                        id="panel1-header"
                                                        className="text"
                                                    >
                                                        GenerateNavTokens
                                                    </YellowAccordionSummary>
                                                    <StyledAccordionDetails>
                                                        <Stack
                                                            gap={0.5}
                                                            sx={{
                                                                mt: 1,
                                                                mb: 2
                                                            }}
                                                        >
                                                            <BulletText>
                                                                static: 
                                                            </BulletText>
                                                            <BulletText>
                                                                async: 
                                                            </BulletText>
                                                            <BulletText>
                                                                access: <span className='code color-blue'>public</span>
                                                            </BulletText>
                                                            <BulletText>
                                                                returns: <span className='code color-light-green'>IEnumerable</span>&lt;<span className='code color-green'>NavToken</span>&gt;
                                                            </BulletText>
                                                            <Typography
                                                                variant='body2'
                                                                sx={{
                                                                    p: 0.75,
                                                                    mt: 1,
                                                                    // border: '1px solid #00e6cf',
                                                                    borderRadius: '4px',
                                                                    backgroundColor: '#666666',
                                                                }}
                                                            >
                                                                This public method can be accessed from anywhere without restriction. It is not a static method so a object reference is needed to use this method.
                                                            </Typography>
                                                        </Stack>
                                                    </StyledAccordionDetails>
                                                </YellowAccordion>
                                            </div>
                                        </Stack>
                                    </StyledAccordionDetails>
                                </GreenAccordion>
                            </div>
                        </Stack>
                    </StyledAccordionDetails>
                </BlueAccordion>
                <FlexBox
                    sx={{
                        minHeight: '8px'
                    }}
                >
                </FlexBox>
            </div>
        </ContentContainer>
    );
}

export default StructureSidebarContent;