import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";

const useStyles = makeStyles((theme) => (
{
	section:
	{
		backgroundColor: "#FAFAFA"
	},
	icon: 
  	{
    	marginRight: theme.spacing(1),
	}
}
));

export default function Features(props: any) 
{

	const classes = useStyles();

	return (
	    <section className={classes.section}>
      		<Container maxWidth="lg">
        		<Box py={8}>
          			<Box mb={8}>
            			<Typography color="primary" variant="button" component="h3" align="center" gutterBottom={true}>Technologies</Typography>
            			<Typography variant="h4" component="h2" align="center">I work primarily with</Typography>
          			</Box>
          			<Grid container spacing={6}>
            				<Grid item xs={12} sm={6}>
              					<Box mb={2} display="flex" alignItems="center">
                					<CodeIcon color="primary" className={classes.icon} />
                					<Typography variant="h5" component="h3">Languages</Typography>
            	  				</Box>
        	      				<Typography variant="body1" component="p" color="textSecondary">
									  Today in my daily job I use <b>C#</b>, <b>JavaScript</b> and <b>TypeScript</b>. In the past I also used: Basic, Assembler, Turbo Pascal, Delphi, VBA.
								</Typography>
    	        			</Grid>
	            		<Grid item xs={12} sm={6}>
              				<Box mb={2} display="flex" alignItems="center">
                				<LibraryBooksIcon color="primary" className={classes.icon} />
                				<Typography variant="h5" component="h3">Frameworks/Libraries</Typography>
            	  			</Box>
        	      			<Typography variant="body1" component="p" color="textSecondary">
								  Back-End: NET Framework 4.5 (for one project), <b>NET Core</b> (since version 2.0). Front-End libraries: jQuery and <b>React/Redux</b>. CSS Frameworks: Bootstrap, Materialize, Bulma, and <b>Material-UI</b>.
							</Typography>
    	        		</Grid>
	            		<Grid item xs={12} sm={6}>
              				<Box mb={2} display="flex" alignItems="center">
                				<StorageIcon color="primary" className={classes.icon} />
                				<Typography variant="h5" component="h3">OR/M</Typography>
            	  			</Box>
        	      			<Typography variant="body1" component="p" color="textSecondary">
								  I have started with Entity Framework and did just one project, in other projects I use <b>Entity Framework Core</b>. I prefer to use lightweight OR/M like Dapper and use <b>SQL/T-SQL</b> when necessary.
							</Typography>
    	        		</Grid>
			            <Grid item xs={12} sm={6}>         
        				    <Box mb={2} display="flex" alignItems="center">
                				<CloudIcon color="primary" className={classes.icon} />
            	    			<Typography variant="h5" component="h3">Cloud Services</Typography>
        	      			</Box>
    	          			<Typography variant="body1" component="p" color="textSecondary">
								  I have experience with <b>Azure Cloud Services</b>: App Services (PaaS), Azure Storage, Azure SQL, CosmosDb (NoSQL), WebJobs, Azure Functions (C#), Application Insights.
					  		</Typography>
	            		</Grid>
          			</Grid>
        		</Box>
      		</Container>
    	</section>
	);

}