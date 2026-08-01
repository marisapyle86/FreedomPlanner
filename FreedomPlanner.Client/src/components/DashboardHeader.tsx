type DashboardHeaderProps = {
  title: string
  subtitle: string
}

function DashboardHeader({ title, subtitle }: DashboardHeaderProps) {
  return (
    <header className="dashboard-header">
      <h1>{title}</h1>
      <p className="subtitle">{subtitle}</p>
    </header>
  )
}

export default DashboardHeader
