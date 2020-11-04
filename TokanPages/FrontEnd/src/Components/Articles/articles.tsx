import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import ArrowRightAltIcon from '@material-ui/icons/ArrowRightAlt';
import Card from '@material-ui/core/Card';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';

const useStyles = makeStyles((theme) => (
{
	section:
	{
		backgroundColor: "#FFFFFF"
	},
    info: 
    {
        height: "100%",
        minHeight: "128px",
    },
    media: 
    {
        height: "128px",
    },
    firstColumn: 
    {
        paddingRight: theme.spacing(2),
        [theme.breakpoints.down("md")]: 
        {
            marginBottom: theme.spacing(2),
            paddingRight: theme.spacing(0),
        }
    }
}));

export default function Articles(props: any) 
{
  
    const classes = useStyles();

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box pt={8} pb={10}>
                    <Container maxWidth="sm">
    	      		    <Box textAlign="center" mb={5}>
        	    		    <Typography variant="h4" component="h2" gutterBottom={true}>Articles</Typography>
            			    <Typography variant="subtitle1" color="textSecondary">
							    I write on regular basis on various technologies I work with. I share the knowledge and experience. I believe everyone will find something interesting.
						    </Typography>
    	          		</Box>
        	    	</Container>
            		<Container maxWidth="lg">
                        <Grid container>
                            <Grid item xs={12} lg={6} className={classes.firstColumn}>
                                <Card className={classes.info} elevation={1}>
                                    <CardContent className={classes.info}>
                                        <Box display="flex" flexDirection="column" height="100%" pt={2} px={2}>
                                            <Typography variant="h5" component="h2" gutterBottom={true}>
                                                Write-ups on .NET Core, Azure and databases.
                                            </Typography>
                                            <Box mt="auto" mb={2}>
                                                <Typography variant="body1" component="p" color="textSecondary">
                                                    Let's dive into Microsoft technology and programming in general. Read about architecture, design patterns, C#, SQL and other languages.
                                                </Typography>
                                            </Box>
                                            <Box textAlign="right">
                                                <Button color="primary" endIcon={<ArrowRightAltIcon />}>
                                                    View list of articles
                                                </Button>
                                            </Box>
                                        </Box>
                                    </CardContent>
                                </Card>
                            </Grid>
                            <Grid item xs={12} lg={6}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={2}>
                                            <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/tokanpages/images/image1.jpg" />
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={2}>
                                            <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/tokanpages/images/image2.jpg" />
                                        </Card>
                                    </Grid>
                                    <Grid item xs={12} md={4}>
                                        <Card elevation={2}>
                                            <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/tokanpages/images/image3.jpg" />
                                        </Card>
                                    </Grid> 
                                    <Grid item xs={12} md={8}>
                                        <Card elevation={2}>
                                            <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/tokanpages/images/image4.jpg" />
                                        </Card>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </Grid>
    	        	</Container>
                </Box>
            </Container>
        </section>
    );

}
