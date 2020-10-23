import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import ShutterSpeedIcon from "@material-ui/icons/ShutterSpeed";
import PublicIcon from "@material-ui/icons/Public";
import StorageIcon from "@material-ui/icons/Storage";

const useStyles = makeStyles((theme) => (
{
  icon: 
  {
    marginRight: theme.spacing(1),
  }
}
));

export default function Component(props: any) 
{

  const classes = useStyles();

  return (
    <section>
      <Container maxWidth="lg">
        <Box py={8}>
          <Box mb={8}>
            <Typography color="primary" variant="button" component="h3" align="center" gutterBottom={true}>Technologies</Typography>
            <Typography variant="h4" component="h2" align="center">I work primarily with below technologies</Typography>
          </Box>
          <Grid container spacing={6}>

            <Grid item xs={12} sm={6}>
              <Box mb={2} display="flex" alignItems="center">
                <VerifiedUserIcon color="primary" className={classes.icon} />
                <Typography variant="h5" component="h3">Languages</Typography>
              </Box>
              <Typography variant="body1" component="p">So far I have used (with this order): Basic, Assembler, Turbo Pascal, Delphi, VBA, C#, JavaScript, TypeScript. In my daily job I use C#, JavaScript and TypeScript.</Typography>
            </Grid>

            <Grid item xs={12} sm={6}>
              <Box mb={2} display="flex" alignItems="center">
                <ShutterSpeedIcon color="primary" className={classes.icon} />
                <Typography variant="h5" component="h3">Frameworks/Libraries</Typography>
              </Box>
              <Typography variant="body1" component="p">Back-End: NET Framework 4.5 (only one project), NET Core (since version 2.0). Front-End libraries: jQuery and React/Redux. CSS Frameworks: Bootstrap, Materialize, Bulma, and Material-UI.</Typography>
            </Grid>

            <Grid item xs={12} sm={6}>
              <Box mb={2} display="flex" alignItems="center">
                <PublicIcon color="primary" className={classes.icon} />
                <Typography variant="h5" component="h3">OR/M</Typography>
              </Box>
              <Typography variant="body1" component="p">I have started with Entity Framework and did only one project, in other projects I use Entity Framework Core. I prefer to use lightweight OR/M with SQL/T-SQL when necessary.</Typography>
            </Grid>

            <Grid item xs={12} sm={6}>         
              <Box mb={2} display="flex" alignItems="center">
                <StorageIcon color="primary" className={classes.icon} />
                <Typography variant="h5" component="h3">Azure Cloud Services</Typography>
              </Box>
              <Typography variant="body1" component="p">I have experience with App Services, Azure Storage, Azure SQL, WebJobs, Azure Functions (C#), Application Insights.</Typography>
            </Grid>

          </Grid>
        </Box>
      </Container>
    </section>
  );

}
