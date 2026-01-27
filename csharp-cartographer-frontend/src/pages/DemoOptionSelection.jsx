import { useState } from 'react';
import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Button, Stack, Typography } from '@mui/material';
import MapOutlinedIcon from '@mui/icons-material/MapOutlined';
import colors from '../utils/colors';

const PageContainer = styled(Box)(() => ({
    minHeight: '100%',
    paddingRight: 2,
    paddingLeft: 2,
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    paddingTop: '100px',
    alignItems: 'center'
}));

const BorderBoxLeft = styled(Box)(() => ({
    display: 'flex',
    height: '33vh',
    borderRight: '1px solid #00e6cf',
}));

const BorderBoxRight = styled(Box)(() => ({
    display: 'flex',
    height: '33vh',
    borderLeft: '1px solid #00e6cf',
}));

const BtnText = styled(Typography)(() => ({
    fontSize: '1.063rem',
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    transition: 'transform 0.3s ease',
    '&:hover': {
          transform: 'translateY(-4px)',
          color: colors.code,
          cursor: 'pointer'
    },
}));

const InfoText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '0.938rem',
}));

const ViewDemoBtn = styled(Button)(() => ({
    height: '45px',
    width: '160px',
    color: '#fff',
    background: 'linear-gradient(to right, #006650, #00b38c)',
    boxShadow: '0px 2px 4px -1px rgba(0, 0, 0, 0.4), 0px 4px 5px 0px rgba(0, 0, 0, 0.28), 0px 1px 10px 0px rgba(0, 0, 0, 0.24)',
    padding: '4px 12px 2px 12px',
    '&:hover': {
        color: '#fff',
        background: 'linear-gradient(to right, #004d3c, #009978)',
    },
}));

const DemoOptionSelection = () => {

    // State Variables
    const [selectedFile, setSelectedFile] = useState("NavToken.cs");

    // Event Handlers
    const handleFileClick = (value) => {
        setSelectedFile(value);
    };

    return (
        <PageContainer>
            <Stack gap={8}>
                <FlexBox>
                    <Stack
                        width={'30%'}
                        alignItems={'center'}
                    >
                        <Stack
                            gap={4.5}
                        >
                            <BtnText
                                onClick={() => handleFileClick("NavToken.cs")}
                                sx={{
                                    color: selectedFile === 'NavToken.cs' ? colors.code : '#fff'
                                }}
                            >
                                [NavToken.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("GenerateArtifactWorkflow.cs")}
                                sx={{
                                    color: selectedFile === 'GenerateArtifactWorkflow.cs' ? colors.code : '#fff',
                                }}
                            >
                                [GenerateArtifactWorkflow.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("SyntaxHighlighter.cs")}
                                sx={{
                                    color: selectedFile === 'SyntaxHighlighter.cs' ? colors.code : '#fff'
                                }}
                            >
                                [SyntaxHighlighter.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("ArtifactRepository.cs")}
                                sx={{
                                    color: selectedFile === 'ArtifactRepository.cs' ? colors.code : '#fff'
                                }}
                            >
                                [ArtifactRepository.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("ArtifactController.cs")}
                                sx={{
                                    color: selectedFile === 'ArtifactController.cs' ? colors.code : '#fff'
                                }}
                            >
                                [ArtifactController.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("StringHelpers.cs")}
                                sx={{
                                    color: selectedFile === 'StringHelpers.cs' ? colors.code : '#fff'
                                }}
                            >
                                [StringHelpers.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("ChatGptProvider.cs")}
                                sx={{
                                    color: selectedFile === 'ChatGptProvider.cs' ? colors.code : '#fff'
                                }}
                            >
                                [ChatGptProvider.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("GenerateArtifactDto.cs")}
                                sx={{
                                    color: selectedFile === 'GenerateArtifactDto.cs' ? colors.code : '#fff'
                                }}
                            >
                                [GenerateArtifactDto.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("SyntaxHighlighterTests.cs")}
                                sx={{
                                    color: selectedFile === 'SyntaxHighlighterTests.cs' ? colors.code : '#fff'
                                }}
                            >
                                [SyntaxHighlighterTests.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("OperatorDemo.cs")}
                                sx={{
                                    color: selectedFile === 'OperatorDemo.cs' ? colors.code : '#fff'
                                }}
                            >
                                [OperatorDemo.cs]
                            </BtnText>
                            <BtnText
                                onClick={() => handleFileClick("CollectionDemo.cs")}
                                sx={{
                                    color: selectedFile === 'CollectionDemo.cs' ? colors.code : '#fff'
                                }}
                            >
                                [CollectionDemo.cs]
                            </BtnText>
                        </Stack>
                    </Stack>

                    <Box>
                        <BorderBoxLeft />
                        <BorderBoxRight />
                    </Box>

                    {selectedFile === "NavToken.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Model Definition
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A model definition represents the data structure for an object. It typically contains properties that define the data fields and sometimes includes methods for data manipulation or validation.
                                    </InfoText>
                                    <InfoText>
                                        It typically contains properties that define the data fields and sometimes includes methods for data manipulation or validation.
                                    </InfoText>
                                    <InfoText>
                                        Model classes are often used in frameworks like ASP.NET Core MVC or Entity Framework to define how data is stored, transmitted, or displayed.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "GenerateArtifactWorkflow.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Workflow Class
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A workflow class is responsible for orchestrating and coordinating a sequence of steps or tasks that make up a business process.
                                    </InfoText>
                                    <InfoText>
                                        It defines the flow of execution, including the logic for handling various conditions, decisions, and transitions between tasks.
                                    </InfoText>
                                    <InfoText>
                                        Workflows are often used in systems that manage complex business processes, ensuring that actions occur in the correct order and under the right conditions.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "SyntaxHighlighter.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Service Class
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A service is a class that contains business logic or operations to process data, interact with external systems (e.g., APIs or databases), or perform complex computations.
                                    </InfoText>
                                    <InfoText>
                                        Services typically serves as a middle layer between the controllers and data models, encapsulating the application's core functionality.
                                    </InfoText>
                                    <InfoText>
                                        In frameworks like ASP.NET Core, service classes are often registered for dependency injection and provide reusable methods for different parts of the application.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "ArtifactRepository.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    DataAccess Class
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A data access class is responsible for interacting with the database or other data storage systems to perform create, read, update, & delete (CRUD) operations.
                                    </InfoText>
                                    <InfoText>
                                        This keeps data access logic separate from business logic and UI layers, promoting maintainability and testability.
                                    </InfoText>
                                    <InfoText>
                                        This class is often referred to as a repository when used with the Repository Pattern.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "ArtifactController.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    API Controller
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        An API controller is a specialized class in ASP.NET Core that handles HTTP requests and responses in a Web API.
                                    </InfoText>
                                    <InfoText>
                                        They are designed to expose endpoints that provide or consume data, typically in JSON or XML format.
                                    </InfoText>
                                    <InfoText>
                                        They perform actions like creating, reading, updating, and deleting (CRUD) resources, and they are commonly used for building RESTful web services.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "StringHelpers.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Extension/Helper Class
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        An extension/helper class is a construct used to provide additional functionality or utility methods that can be applied to existing classes or objects.
                                    </InfoText>
                                    <InfoText>
                                        When utilized properly, extension classes are designed to be reusable across all areas of an application. 
                                    </InfoText>
                                    <InfoText>
                                        Helper methods are often declared as static, meaning they can be called directly without creating an instance of the helper class.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "ChatGptProvider.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    External Client
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        An external client is a class that encapsulates communication with an external system or service, such as a web API, messaging system, or third-party service.
                                    </InfoText>
                                    <InfoText>
                                        This class is responsible for handling the details of interaction with the external system, such as building & sending requests and processing their responses.
                                    </InfoText>
                                    <InfoText>
                                        These classes abstract communication protocols like Http or TCP, ensuring that the consuming code doesn't need to manage these details.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "GenerateArtifactDto.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Data Transfer Object (DTO)
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A Data Transfer Object is a simple object designed to carry data between layers or components of an application.
                                    </InfoText>
                                    <InfoText>
                                        Unlike domain models, DTOs typically do not contain business logic and are solely focused on encapsulating and serializing data for transfer.
                                    </InfoText>
                                    <InfoText>
                                        They often represent a subset or a projection of the underlying data to avoid overfetching or exposing sensitive internal fields.
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "SyntaxHighlighterTests.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    UnitTest Class
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                        A unit test class...
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "OperatorDemo.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Operator Demo
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : selectedFile === "CollectionDemo.cs"
                        ? (
                            <Stack
                                width={'40%'}
                                gap={3}
                                sx={{
                                    px: 8
                                }}
                            >
                                <InfoText
                                    className='color-code'
                                    textAlign={"center"}
                                    sx={{
                                        fontSize: '1.65rem'
                                    }}
                                >
                                    Collection Demo
                                </InfoText>
                                <Stack
                                    display={'flex'}
                                    gap={4}
                                    sx={{
                                        bgcolor: colors.gray25,
                                        padding: 2,
                                        borderRadius: '5px'
                                    }}
                                >
                                    <InfoText>
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                    <InfoText>
                                    </InfoText>
                                </Stack>
                            </Stack>
                        )
                        : (
                            <></>
                        )
                    }

                    <Box>
                        <BorderBoxLeft />
                        <BorderBoxRight />
                    </Box>

                    <Box
                        display="flex"
                        justifyContent="center"
                        width='30%'
                    >
                        <Stack
                            display="flex"
                            alignItems="center"
                            gap={4}
                            sx={{
                                px: 6,
                            }}
                        >
                            <Typography
                                className='code'
                                sx={{
                                    fontSize: '0.875rem'
                                }}
                            >
                                All demo artifacts were created using C# source code files from this application.
                            </Typography>
                            <Typography
                                className='code'
                                sx={{
                                    fontSize: '0.875rem'
                                }}
                            >
                                Try generating a brand new artifact by uploading your own C# code <a className='color-code' href="/upload">here</a>. 
                            </Typography>
                            <Link
                                to={`/cartographer-demo?file=${encodeURIComponent(selectedFile)}`}
                                style={{ textDecoration: 'none' }}
                            >
                                <ViewDemoBtn
                                    size='large'
                                    startIcon={<MapOutlinedIcon sx={{ color: '#fff' }} />}
                                    sx={{
                                        mt: 4
                                    }}
                                >
                                    View Demo
                                </ViewDemoBtn>
                            </Link>
                        </Stack>
                    </Box>
                </FlexBox>
            </Stack>
        </PageContainer>
    );
}

export default DemoOptionSelection;