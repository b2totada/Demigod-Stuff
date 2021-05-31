<?php
    include_once 'header.php';
?>



<div class="container">
<?php
    if (isset($_SESSION["useruid"]))
    {
        echo "<h4>Hello there " . $_SESSION["useruid"] . "</h4>";
    }
?>


<div class="container" id="MainContent">
<h1>
  Blade Master<br>
  <small>2D Action Platformer</small>
</h1>
  <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
    <!-- Indicators -->
    <ol class="carousel-indicators">
      <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
      <li data-target="#carousel-example-generic" data-slide-to="1"></li>
      <li data-target="#carousel-example-generic" data-slide-to="2"></li>
      <li data-target="#carousel-example-generic" data-slide-to="3"></li>
      <li data-target="#carousel-example-generic" data-slide-to="4"></li>
      <li data-target="#carousel-example-generic" data-slide-to="5"></li>
    </ol>

    <!-- Wrapper for slides -->
    <div class="carousel-inner" role="listbox">
      <div class="item active">
        <img src="img/Screenshot01.PNG">
        <div class="carousel-caption">
          <h4>Simple, clean layout</h4>
        </div>
      </div>

      <div class="item">
        <img src="img/Screenshot08.PNG">
        <div class="carousel-caption">
          <h4>Challanging platforming</h4>
        </div>
      </div>

      <div class="item">
        <img src="img/Screenshot04.PNG">
        <div class="carousel-caption">
          <h4>Fast-paced combat</h4>
        </div>
      </div>

      <div class="item">
        <img src="img/Screenshot05.PNG">
        <div class="carousel-caption">
          <h4>Stunning visuals and next-gen graphics</h4>
        </div>
      </div>

      <div class="item">
        <img src="img/Screenshot06.PNG">
        <div class="carousel-caption">
          <h4>Breathtaking ambience</h4>
        </div>
      </div>

      <div class="item">
        <img src="img/Screenshot07.PNG">
        <div class="carousel-caption">
          <h4>Immersive gameplay</h4>
        </div>
      </div>
    </div>

    <!-- Controls -->
    <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>

    <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
  </div>
</div>
</div>

<?php
    include_once 'footer.php';
?>