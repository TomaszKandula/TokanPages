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

export default function Component(props: any) 
{

  const classes = useStyles();

  return (
    <img src="https://maindbstorage.blob.core.windows.net/images/Tomek_Bergen_Alt.jpg" alt="" className={classes.image} />
  );

}