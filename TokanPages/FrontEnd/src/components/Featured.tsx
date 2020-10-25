import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardActionArea from "@material-ui/core/CardActionArea";
import CardMedia from "@material-ui/core/CardMedia";
import CardHeader from "@material-ui/core/CardHeader";

const useStyles = makeStyles((theme) => (
{
  media: 
  {
    height: "256px"
  }
}));

export default function Component(props: any) 
{
  
  const classes = useStyles();

  return (
    <section>
      <Box pt={8} pb={10}>
        <Container maxWidth="sm">
          <Box textAlign="center" mb={5}>
            <Typography variant="h4" component="h2" gutterBottom={true}>Featured</Typography>
            <Typography variant="subtitle1" color="textSecondary">Below you can find my lastest articles published on different portals like Medium.com or LinkedIn.</Typography>
          </Box>
        </Container>
        <Container maxWidth="md">
          <Box textAlign="center">

            <Grid container spacing={4}>

              <Grid item xs={12} md={4}>
                <Card>
                  <CardActionArea href="http://geek.justjoin.it/wysokie-widelki-clickbait-devdebata" target="_blank">
                    <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/images/Article3.png" />
                    <CardHeader title="Just Geek IT" subheader="Widełki płacowe stanowią standard w branży..." titleTypographyProps={{gutterBottom: true}}/>
                  </CardActionArea>
                </Card>
              </Grid>

              <Grid item xs={12} md={4}>
                <Card>
                  <CardActionArea href="https://medium.com/@tomasz.kandula/sql-injection-1bde8bb76ebc" target="_blank">
                    <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/images/Article2.png" />
                    <CardHeader title="SQL Injection" subheader="This article will explore the issue in greater detail..." titleTypographyProps={{gutterBottom: true}} />
                  </CardActionArea>
                </Card>
              </Grid>

              <Grid item xs={12} md={4}>
                <Card>
                  <CardActionArea href="https://medium.com/@tomasz.kandula/i-said-goodbye-to-stored-procedures-539d56350486" target="_blank">
                    <CardMedia className={classes.media} image="https://maindbstorage.blob.core.windows.net/images/Article1.png" />
                    <CardHeader title="Stored Procedures" subheader="I explain why I do not need them that much..." titleTypographyProps={{gutterBottom: true}} />
                    </CardActionArea>
                    </Card>
                  </Grid>

              </Grid>
              </Box>
            </Container>
          </Box>
        </section>
      );

}
