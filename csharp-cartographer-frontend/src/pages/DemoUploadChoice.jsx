import { Link } from 'react-router-dom';
import { styled } from '@mui/material/styles';
import { Box, Typography } from '@mui/material';
import colors from '../utils/colors';

const PageContainer = styled(Box)(() => ({
    minHeight: '100%',
}));

const FlexBox = styled(Box)(() => ({
    display: 'flex',
    paddingTop: '80px',
    alignItems: 'center'
}));

const BorderBoxLeft = styled(Box)(() => ({
    display: 'flex',
    height: '35vh',
    borderRight: '1px solid #00e6cf',
}));

const BorderBoxRight = styled(Box)(() => ({
    display: 'flex',
    height: '35vh',
    borderLeft: '1px solid #00e6cf',
}));

const BtnText = styled(Typography)(() => ({
    fontFamily: 'Consolas, Input, DejaVu Sans Mono',
    fontSize: '2rem',
    transition: 'transform 0.3s ease',
    '&:hover': {
          transform: 'translateY(-4px)',
          color: colors.code,
    },
}));

const DemoUploadChoice = () => {
    return (
        <PageContainer>
            <FlexBox>
                <Box
                    display={'flex'}
                    width={'50%'}
                    justifyContent={'center'}
                >
                    <Link to="/demo-options">
                        <BtnText>
                            [Demo]
                        </BtnText>
                    </Link>
                </Box>
                <Box>
                    <BorderBoxLeft />
                    <BorderBoxRight />
                </Box>
                <Box
                    display={'flex'}
                    width={'50%'}
                    justifyContent={'center'}
                >
                    <Link to="/upload">
                        <BtnText>
                            [Upload]
                        </BtnText>
                    </Link>
                </Box>
            </FlexBox>
        </PageContainer>
    );
}

export default DemoUploadChoice;