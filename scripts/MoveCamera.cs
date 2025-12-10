using Godot;

public partial class MoveCamera : Camera2D
{
    [Export] private InputManager _inputManager;
    [Export] private TextureRect _canvas;

    private Vector2 _startPos;
    private Vector2 _lastPos;
    private Vector2 _minLimit;
    private Vector2 _maxLimit;
    
    private bool _canMove = false;
    private ProjectSettings _settings;
    
    private float _minZoom = 0.1f;
    private float _maxZoom;

    public override void _Ready()
    {
        _inputManager.OnClickLeftMouseDown += SetStartPosition;
        _inputManager.OnClickLeftMouseUp += ResetMove;
        _inputManager.OnMouseMove += Move;

        _inputManager.OnMouseScrollDown += ZoomOut;
        _inputManager.OnMouseScrollUp += ZoomIn;

        _startPos = Position;
        _settings = ProjectSettings.Instance;

        _canvas.ItemRectChanged += ComputeZoomLimits;

        ComputeZoomLimits();
    }
    
    private void ComputeZoomLimits()
    {
        Vector2 viewport = GetViewportRect().Size;
        Vector2 canvasSize = _canvas.Size;

        float scaleX = canvasSize.X / viewport.X;
        float scaleY = canvasSize.Y / viewport.Y;

        float fitScale = Mathf.Max(scaleX, scaleY);
        
        float fitZoom = 1f / fitScale;

        // allow 2× extra zoom out
        _minZoom = fitZoom * _settings.MinZoom;

        // allow 4× zoom in
        _maxZoom = fitZoom *_settings.MaxZoom;

        Zoom = Vector2.One * fitZoom;

        CenterCamera();
        CheckBounds();
    }

    private void CenterCamera()
    {
        Position = _startPos;
    }

    private void SetStartPosition(Vector2 position)
    {
        _lastPos = position;
        _canMove = true;
    }

    private void ResetMove(Vector2 position)
    {
        _canMove = false;
    }

    private void Move(Vector2 position)
    {
        if(!_canMove)
            return;

        Vector2 drag = (_lastPos - position) / Zoom;
        Position += drag;

        _lastPos = position;
        
        ClampToCanvas();
    }

    private void ZoomIn()
    {
        Zoom += Vector2.One * _settings.ZoomSpeed;
        Zoom = Zoom.Clamp(Vector2.One * _minZoom, Vector2.One * _maxZoom);
        CheckBounds();
    }

    private void ZoomOut()
    {
        Zoom -= Vector2.One * _settings.ZoomSpeed;
        Zoom = Zoom.Clamp(Vector2.One * _minZoom, Vector2.One * _maxZoom);
        CheckBounds();
    }

    private void CheckBounds()
    {
        Vector2 viewport = GetViewportRect().Size;
        Vector2 halfView = (viewport * 0.5f) / Zoom;

        Vector2 canvasPos = _canvas.GlobalPosition;
        Vector2 canvasSize = _canvas.Size;

        float left = canvasPos.X + halfView.X;
        float right = canvasPos.X + canvasSize.X - halfView.X;

        float top = canvasPos.Y + halfView.Y;
        float bottom = canvasPos.Y + canvasSize.Y - halfView.Y;
        
        _minLimit = new Vector2(Mathf.Min(left, right), Mathf.Min(top, bottom));
        _maxLimit = new Vector2(Mathf.Max(left, right), Mathf.Max(top, bottom));
        
        ClampToCanvas();
    }
    
    private void ClampToCanvas()
    {
        Position = new Vector2(
            Mathf.Clamp(Position.X, _minLimit.X, _maxLimit.X),
            Mathf.Clamp(Position.Y, _minLimit.Y, _maxLimit.Y)
        );
    }

}