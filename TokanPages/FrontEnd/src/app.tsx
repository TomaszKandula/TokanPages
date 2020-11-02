import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; 
import { makeStyles } from "@material-ui/core/styles";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import MainPage from "./Pages/mainPage";
import MyStory from "./Pages/myStory"; 
import Terms from "./Pages/terms";
import Policy from "./Pages/policy";

interface Props 
{
	children: React.ReactElement;
}

const useStyles = makeStyles((theme) => (
{
	scrolltotop: 
	{
		position: "fixed",
		bottom: theme.spacing(2),
		right: theme.spacing(2),
	}
}));

function ScrollTop(props: Props) 
{

	const { children } = props;
	const classes = useStyles();

	const trigger = useScrollTrigger(
	{
	  	disableHysteresis: true,
	  	threshold: 100
	});
  
	const handleClick = (event: React.MouseEvent<HTMLDivElement>) => 
	{

		const anchor = ((event.target as HTMLDivElement).ownerDocument || document).querySelector("#back-to-top-anchor");
  
		if (anchor) 
	  	{
            anchor.scrollIntoView({ behavior: "smooth"});
        }

	};
  
	return (
	  <Zoom in={trigger}>
		<div onClick={handleClick} role="presentation" className={classes.scrolltotop}>
		  	{children}
		</div>
	  </Zoom>
	);
}

export default function App() 
{

    return (

        <>
            <Router>
                <Switch>
                  <Route exact path="/"><MainPage /></Route>
                  <Route exact path="/mystory"><MyStory /></Route>
                  <Route exact path="/terms"><Terms /></Route>
                  <Route exact path="/policy"><Policy /></Route>
                </Switch>
            </Router>
            <ScrollTop>
                <Fab color="primary" size="small" aria-label="scroll back to top">
                    <KeyboardArrowUpIcon/>
                </Fab>
            </ScrollTop>
      </>

    );

};
