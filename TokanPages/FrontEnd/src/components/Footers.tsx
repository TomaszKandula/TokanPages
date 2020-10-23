import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import IconButton from "@material-ui/core/IconButton";
import GitHubIcon from "@material-ui/icons/GitHub";
import LinkedInIcon from "@material-ui/icons/LinkedIn";
import InstagramIcon from "@material-ui/icons/Instagram";

const useStyles = makeStyles((theme) => (
{

  root: 
  {
    [theme.breakpoints.down("md")]: 
    {
      textAlign: "center",
    }
  },
  iconsBoxRoot: 
  {
    [theme.breakpoints.down("md")]: 
    {
      width: "100%",
      marginBottom: theme.spacing(2),
    }
  },
  copy: 
  {
    [theme.breakpoints.down("md")]: 
    {
      width: "100%",
      order: 12,
    }
  }
}
));

export default function Component(props: any) 
{
  
  const classes = useStyles();

  return (
    <footer className={classes.root}>
      <Container maxWidth="lg">
        <Box py={6} display="flex" flexWrap="wrap" alignItems="center">
          <Typography color="textSecondary" component="p" gutterBottom={false} className={classes.copy}>© 2020 Tomasz Kandula. All rights reserved.</Typography>
          <Box ml="auto" className={classes.iconsBoxRoot}>
            <IconButton color="secondary" aria-label="GitHub">
              <GitHubIcon />
            </IconButton>
            <IconButton color="secondary" aria-label="LinkedIn">
              <LinkedInIcon />
            </IconButton>
            <IconButton color="secondary" aria-label="Instagram">
              <InstagramIcon />
            </IconButton>
          </Box>
        </Box>
      </Container>
    </footer>
  );

}
