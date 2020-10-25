import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Link from "@material-ui/core/Link";

const useStyles = makeStyles((theme) => (
{
  toolbar: { justifyContent: "center", }
}));

export default function Navigation(props: { content: any; }) 
{

  const classes = useStyles();

  const content = 
  {
    "brand": { image: "mui-assets/img/logo-pied-piper-white.png", width: 120 },
    ...props.content
  };

  let brand;

  if (content.brand.image) 
  {
    brand = <img src={ content.brand.image } alt="" width={ content.brand.width } />;
  } else 
  {
    brand = content.brand.text || '';
  }

  return (
    <AppBar position="static">
      <Toolbar className={classes.toolbar}>
        <Link href="#" variant="h5" color="inherit" underline="none">
          {brand}
        </Link>
      </Toolbar>
    </AppBar>
  );

}
