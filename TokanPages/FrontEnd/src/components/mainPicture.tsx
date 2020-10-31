import React from "react";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => (
{
	image: 
  	{
    	maxWidth: "100%",
	}
}
));

export default function MainPicture(props: any) 
{

	const classes = useStyles();

	return (
    	<img src="https://maindbstorage.blob.core.windows.net/images/tomek_bergen.jpg" alt="" className={classes.image} />
	);

}
