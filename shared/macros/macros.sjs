macro (class) {
  rule {
    $className {
      $(function $methodName $methodParams $methodBody) ...
    }
  } => {
    function $className() {
      $(this.$methodName =
          function $methodName $methodParams $methodBody;) ...
    }
  }
}