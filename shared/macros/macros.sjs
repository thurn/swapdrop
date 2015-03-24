macro class {
  rule {
    $className {
      $(function $methodName $methodParams $methodBody) ...
    }
  } => {
    function $className() {
      $($className.prototype.$methodName =
          function $methodName $methodParams $methodBody;) ...
    }
  }
}

macro # {
  rule {
    pragma strict
  } => {
  }
}
